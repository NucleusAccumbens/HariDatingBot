using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.AdminCommands.CallbackAdminCommands;

public class VerifidePhotoCallbackCommand : BaseCallbackCommand
{
    private readonly string _userAlertPhotoIsVerifide =
    "the photo has been verified and will appear on your profile";

    private readonly string _userAlertPhoto =
        "photo not verified";

    private readonly string _userIsntInDb = "пользователь удалил профиль";

    private readonly InlineKeyboardMarkup _inlineKeyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 view photos  ", callbackData: $"uViewPhotos"),
        },
    });

    private readonly IUpdatePhotoCommand _updatePhotoCommand;

    private readonly IDeletePhotoCommand _deletePhotoCommand;

    private readonly IUpdateDatingUserCommand _updateDatingUserCommand;

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;


    public VerifidePhotoCallbackCommand(IUpdatePhotoCommand updatePhotoCommand, 
        IDeletePhotoCommand deletePhotoCommand, IUpdateDatingUserCommand updateDatingUserCommand, 
        ICheckUserHasPhotosQuery checkUserHasPhotosQuery)
    {
        _updatePhotoCommand = updatePhotoCommand;
        _deletePhotoCommand = deletePhotoCommand;
        _updateDatingUserCommand = updateDatingUserCommand;
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
    }

    public override char CallbackDataCode => 't';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            string data = update.CallbackQuery.Data;

            if (data.Contains("tYes"))
            {
                long userChatId = await _updatePhotoCommand
                    .UpdarePhotoIsVerifideAsync(GetPhotoIdToYes(data));

                if (userChatId != 0)
                {

                    bool hasPhoto = await _checkUserHasPhotosQuery.CheckUserHasPhotosAsync(userChatId);

                    if (!hasPhoto)
                    {
                        await _updateDatingUserCommand.UpdateDatingUserHasAPhotoAsync(userChatId);
                    }

                    await MessageService.ShowAllert(callbackId, client,
                        "фото верефицировано и появится в профиле пользователя");

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    await MessageService.SendMessage(userChatId, client, _userAlertPhotoIsVerifide,
                        _inlineKeyboardMarkup);

                    return;
                }

                else await MessageService.ShowAllert(callbackId, client, _userIsntInDb);

                return;
            }
            if (data.Contains("tNo"))
            {
                long userChatId = await _deletePhotoCommand
                    .DeletePhotoAsync(GetPhotoIdToNo(data));

                if (userChatId != 0)
                {
                    await MessageService.ShowAllert(callbackId, client,
                        "фото удалено из базы данных");

                    await MessageService.DeleteMessage(chatId, messageId, client);

                    await MessageService.SendMessage(userChatId, client, _userAlertPhoto, null);
                }

                else await MessageService.ShowAllert(callbackId, client, _userIsntInDb);
            }
        }
    }

    private static long GetPhotoIdToYes(string data)
    {
        return Convert
            .ToInt64(data[4..]);
    }

    private static long GetPhotoIdToNo(string data)
    {
        return Convert
            .ToInt64(data[3..]);
    }
}
