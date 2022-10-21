using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;
using Telegram.Bot.Types;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ViewPhotosCallbackCommand : BaseCallbackCommand
{
    private readonly string _allert = "there are no photos in your profile";

    private readonly ViewPhotoMessage _viewPhotoMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;

    private readonly IGetPhotosQuery _getPhotosQuery;

    public ViewPhotosCallbackCommand(ICheckUserHasPhotosQuery checkUserHasPhotosQuery,
        IGetPhotosQuery getPhotosQuery, IMemoryCachService memoryCachService)
    {
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
        _getPhotosQuery = getPhotosQuery;
        _memoryCachService = memoryCachService;
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

                    await _viewPhotoMessage.SendPhoto(chatId, client, photos[photos.Count - 1].PathToPhoto);
                }
            }
            if (data.Contains("uBack"))
            {
                var photo = _memoryCachService
                    .GetCurrentPhotoToGoBackFromMemoryCach(chatId);

                await _viewPhotoMessage
                    .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                return;
            }
            if (data == "uNext")
            {
                var photo = _memoryCachService.GetCurrentPhotoFromMemoryCach(chatId);

                await _viewPhotoMessage
                    .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                return;
            }

        }
    }

    private async Task SendPhotos(long chatId, ITelegramBotClient client, List<Photo> photos)
    {
        List<InputMediaPhoto> photoSizes = new();

        foreach (var photo in photos)
        {
            photoSizes.Add(
                new InputMediaPhoto(new InputMedia(photo.PathToPhoto)));
        }

        await MessageService.SendMediaGroup(chatId, client, photoSizes);
    }
}
