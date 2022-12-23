using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ViewPhotosCallbackCommand : BaseCallbackCommand
{
    private readonly string _allert = "there are no photos in your profile";

    private readonly string _noProfileMessage =
        "you haven't created a profile yet. click /start to register";

    private ViewPhotoMessage _viewPhotoMessage;

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;

    private readonly IGetPhotosQuery _getPhotosQuery;

    private readonly IDeletePhotoCommand _deletePhotoCommand;

    private readonly IUpdateDatingUserCommand _updateDatingUserCommand;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public ViewPhotosCallbackCommand(ICheckUserHasPhotosQuery checkUserHasPhotosQuery,
        IGetPhotosQuery getPhotosQuery, IMemoryCachService memoryCachService, IDeletePhotoCommand deletePhotoCommand,
        IUpdateDatingUserCommand updateDatingUserCommand, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
        _getPhotosQuery = getPhotosQuery;
        _memoryCachService = memoryCachService;
        _deletePhotoCommand = deletePhotoCommand;
        _updateDatingUserCommand = updateDatingUserCommand;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override char CallbackDataCode => 'u';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.ShowAllert(callbackId, client, _noProfileMessage);

                return;
            }

            string data = update.CallbackQuery.Data;

            if (data == "uViewPhotos")
            {
                if (await _checkUserHasPhotosQuery
                .CheckUserHasPhotosAsync(chatId) == false) await MessageService
                    .ShowAllert(callbackId, client, _allert);

                else
                {
                    var photos = await _getPhotosQuery.GetUserPhotosAsync(chatId);

                    if (photos.Count == 1)
                    {
                        _viewPhotoMessage = new(photos[^1].Id, true);

                        await _viewPhotoMessage.SendPhoto(chatId, client, photos[^1].PathToPhoto);

                        return;
                    }

                    _memoryCachService.SetMemoryCach(chatId, photos);

                    _viewPhotoMessage = new(photos[^1].Id, false);

                    await _viewPhotoMessage.SendPhoto(chatId, client, photos[^1].PathToPhoto);
                }
            }
            if (data.Contains("uBack"))
            {
                var photo = _memoryCachService
                    .GetCurrentPhotoToGoBackFromMemoryCach(chatId);

                _viewPhotoMessage = new(photo.Id, false);

                await _viewPhotoMessage
                    .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                return;
            }
            if (data == "uNext")
            {
                var photo = _memoryCachService.GetCurrentPhotoFromMemoryCach(chatId);

                _viewPhotoMessage = new(photo.Id, false);

                await _viewPhotoMessage
                    .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                return;
            }
            if (data.Contains("uDelete"))
            {
                await _deletePhotoCommand.DeletePhotoAsync(GetPhotoId(data));

                var photos = await _getPhotosQuery.GetUserPhotosAsync(chatId);

                if (photos == null || photos.Count == 0)
                {
                    await _updateDatingUserCommand.UpdateDatingUserHasAPhotoAsync(chatId);
                    
                    await MessageService.DeleteMessage(chatId, messageId, client);
                }

                if (photos != null && photos.Count > 1)
                {
                    _memoryCachService.SetMemoryCach(chatId, photos);

                    _viewPhotoMessage = new(photos[^1].Id, false);

                    await _viewPhotoMessage.EditMediaMessage(chatId, messageId, client,
                        photos[^1].PathToPhoto);
                }

                if (photos != null && photos.Count == 1)
                {
                    _viewPhotoMessage = new(photos[^1].Id, true);

                    await _viewPhotoMessage.EditMediaMessage(chatId, messageId, client,
                        photos[^1].PathToPhoto);
                }

                await MessageService.ShowAllert(callbackId, client, "the photo has been deleted");

            }
        }
    }

    private long GetPhotoId(string data)
    {
        return Convert.ToInt64(data[7..]);
    }
}
