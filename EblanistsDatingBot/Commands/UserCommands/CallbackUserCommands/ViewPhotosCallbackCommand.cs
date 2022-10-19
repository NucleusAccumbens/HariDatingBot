using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ViewPhotosCallbackCommand : BaseCallbackCommand
{
    private readonly string _allert = "there are no photos in your profile";

    private readonly PhotoManagementMessage _photoManagementMessage = new();

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;

    private readonly IGetPhotosQuery _getPhotosQuery;

    public ViewPhotosCallbackCommand(ICheckUserHasPhotosQuery checkUserHasPhotosQuery,
        IGetPhotosQuery getPhotosQuery)
    {
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
        _getPhotosQuery = getPhotosQuery;
    }

    public override char CallbackDataCode => 'u';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserHasPhotosQuery
                .CheckUserHasPhotosAsync(chatId) == false) await MessageService
                    .ShowAllert(callbackId, client, _allert);

            else
            {
                await SendPhotos(chatId, client,
                    await _getPhotosQuery.GetUserPhotosAsync(chatId));

                await _photoManagementMessage.SendMessage(chatId, client);
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
