using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.GeneralCommands.CallbackGeneralCommands;

public class StartCallbackCommand : BaseCallbackCommand
{
    private readonly NavigationMessage _message = new();
    
    public override char CallbackDataCode => '_';

    public async override Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _message.EditMessage(chatId, messageId, client);
        }
    }
}
