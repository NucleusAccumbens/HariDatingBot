using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ProfileCallbackCommand : BaseCallbackCommand
{
    private ProfileMessage _profileMessage;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    public ProfileCallbackCommand(IGetDatingUserQuery getDatingUserQuery)
    {
        _getDatingUserQuery = getDatingUserQuery;    
    }
    
    public override char CallbackDataCode => 'o';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);

            if (user != null) _profileMessage = new(user.Adapt<DatingUserDto>());

            await _profileMessage.EditMessage(chatId, messageId, client);
        }
    }
}
