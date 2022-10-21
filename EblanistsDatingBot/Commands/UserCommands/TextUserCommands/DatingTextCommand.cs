using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class DatingTextCommand : BaseTextCommand
{
    private readonly DatingMessage _datingMessage = new(true);

    public override string Name => "/dating";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            await _datingMessage.SendMessage(chatId, client);

        }
    }
}
