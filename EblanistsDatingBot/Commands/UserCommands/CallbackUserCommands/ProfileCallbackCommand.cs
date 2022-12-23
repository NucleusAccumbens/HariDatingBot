using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ProfileCallbackCommand : BaseCallbackCommand
{
    private readonly string _noProfileMessage =
        "you haven't created a profile yet. click /start to register";

    private ProfileMessage _profileMessage;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public ProfileCallbackCommand(IGetDatingUserQuery getDatingUserQuery, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _getDatingUserQuery = getDatingUserQuery;  
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }
    
    public override char CallbackDataCode => 'o';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.ShowAllert(callbackId, client, _noProfileMessage);

                return;
            }

            var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);

            if (update.CallbackQuery.Data == "oProfile_")
            {
                if (user != null) _profileMessage = new(user.Adapt<DatingUserDto>(), true);

                await _profileMessage.EditMessage(chatId, messageId, client);

                return;
            }

            if (user != null) _profileMessage = new(user.Adapt<DatingUserDto>(), false);

            await _profileMessage.EditMessage(chatId, messageId, client);
        }
    }
}
