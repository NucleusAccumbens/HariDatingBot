using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ManagePhotoCallbackCommand : BaseCallbackCommand
{

    private readonly IGetPhotosQuery _getPhotosQuery;

    private readonly IDeletePhotoCommand _deletePhotoCommand;

    public ManagePhotoCallbackCommand(IGetPhotosQuery getPhotosQuery, IDeletePhotoCommand deletePhotoCommand)
    {
        _getPhotosQuery = getPhotosQuery;
        _deletePhotoCommand = deletePhotoCommand;
    }

    public override char CallbackDataCode => 'v';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            string callbackId = update.CallbackQuery.Id;

            if (data == "vManagePhotos")
            {
                await SendPhotos(chatId, client, await _getPhotosQuery.GetUserPhotosAsync(chatId));
                
                return;
            }
            if (data.Contains("vDelete"))
            {
                await _deletePhotoCommand
                    .DeletePhotoAsync(GetPhotoId(data));

                await MessageService.ShowAllert(callbackId, client, "photo deleted");

                await MessageService.DeleteMessage(chatId, messageId, client);
            }
        }
    }

    private async Task SendPhotos(long chatId, ITelegramBotClient client, List<Photo> photos)
    {
        foreach (var photo in photos)
        {
            await MessageService
                .SendMessage(chatId, client, String.Empty, photo.PathToPhoto, 
                GetInlineKeyboardMarkup(photo.Id));
        }
    }

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long photoId)
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 delete", callbackData: $"vDelete{photoId}")
            },
        });
    }

    private long GetPhotoId(string data)
    {
        return Convert.ToInt64(data[7..]);
    }
}
