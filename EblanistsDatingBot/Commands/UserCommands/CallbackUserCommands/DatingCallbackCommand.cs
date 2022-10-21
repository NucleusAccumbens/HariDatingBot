using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class DatingCallbackCommand : BaseCallbackCommand
{
    private readonly DatingMessage _datingMessage = new(false);
    
    public override char CallbackDataCode => 'n';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _datingMessage.EditMessage(chatId, messageId, client);

        }
    }
}
