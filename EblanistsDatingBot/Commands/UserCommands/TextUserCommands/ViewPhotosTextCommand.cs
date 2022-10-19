using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class ViewPhotosTextCommand : BaseTextCommand
{
    private readonly string _allert = "there are no photos in your profile";

    private readonly PhotoManagementMessage _photoManagementMessage = new();

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;

    private readonly IGetPhotosQuery _getPhotosQuery;

    public ViewPhotosTextCommand(IGetPhotosQuery getPhotosQuery, 
        ICheckUserHasPhotosQuery checkUserHasPhotosQuery)
    {
        _getPhotosQuery = getPhotosQuery;
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
    }

    public override string Name => "/view_photos";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int messageId = update.Message.MessageId;

            if (await _checkUserHasPhotosQuery.CheckUserHasPhotosAsync(chatId))
            {
                var photos = await _getPhotosQuery.GetUserPhotosAsync(chatId);

                await SendPhotos(chatId, client, photos);

                await _photoManagementMessage.SendMessage(chatId, client);

                return;
            }

            await MessageService.SendMessage(chatId, client, _allert, null);
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
