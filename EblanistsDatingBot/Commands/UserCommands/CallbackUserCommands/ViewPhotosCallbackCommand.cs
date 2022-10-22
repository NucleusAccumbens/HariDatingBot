using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ViewPhotosCallbackCommand : BaseCallbackCommand
{
    private readonly string _allert = "there are no photos in your profile";

    private ViewPhotoMessage _viewPhotoMessage;

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;

    private readonly IGetPhotosQuery _getPhotosQuery;

    private readonly IDeletePhotoCommand _deletePhotoCommand;

    public ViewPhotosCallbackCommand(ICheckUserHasPhotosQuery checkUserHasPhotosQuery,
        IGetPhotosQuery getPhotosQuery, IMemoryCachService memoryCachService, IDeletePhotoCommand deletePhotoCommand)
    {
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
        _getPhotosQuery = getPhotosQuery;
        _memoryCachService = memoryCachService;
        _deletePhotoCommand = deletePhotoCommand;
    }

    public override char CallbackDataCode => 'u';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            string data = update.CallbackQuery.Data;

            if (data == "uViewPhotos")
            {
                if (await _checkUserHasPhotosQuery
                .CheckUserHasPhotosAsync(chatId) == false) await MessageService
                    .ShowAllert(callbackId, client, _allert);

                else
                {
                    var photos = await _getPhotosQuery.GetUserPhotosAsync(chatId);

                    _memoryCachService.SetMemoryCach(chatId, photos);

                    _viewPhotoMessage = new(photos[^1].Id);

                    await _viewPhotoMessage.SendPhoto(chatId, client, photos[^1].PathToPhoto);
                }
            }
            if (data.Contains("uBack"))
            {
                var photo = _memoryCachService
                    .GetCurrentPhotoToGoBackFromMemoryCach(chatId);

                _viewPhotoMessage = new(photo.Id);

                await _viewPhotoMessage
                    .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                return;
            }
            if (data == "uNext")
            {
                var photo = _memoryCachService.GetCurrentPhotoFromMemoryCach(chatId);

                _viewPhotoMessage = new(photo.Id);

                await _viewPhotoMessage
                    .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                return;
            }
            if (data.Contains("uDelete"))
            {
                await _deletePhotoCommand.DeletePhotoAsync(GetPhotoId(data));

                var photos = await _getPhotosQuery.GetUserPhotosAsync(chatId);

                _memoryCachService.SetMemoryCach(chatId, photos);

                _viewPhotoMessage = new(photos[^1].Id);

                await _viewPhotoMessage.EditMediaMessage(chatId, messageId, client,
                    photos[^1].PathToPhoto);

                await MessageService.ShowAllert(callbackId, client, "the photo has been deleted");
                
                return;
            }
        }
    }

    private long GetPhotoId(string data)
    {
        return Convert.ToInt64(data[7..]);
    }
}
