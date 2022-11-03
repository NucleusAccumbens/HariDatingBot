namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class HelpTextCommand : BaseTextCommand
{
    private readonly HelpMessage _helpMessage = new();
    
    public override string Name => "/help";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            await _helpMessage.SendMessage(chatId, client);
        }
    }
}
