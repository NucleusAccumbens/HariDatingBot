using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class FeedbackTextCommand : BaseTextCommand
{
    private readonly FeedbackMessage _feedbackMessage = new();
    
    public override string Name => "/feedback";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            await _feedbackMessage.SendMessage(chatId, client);
        }
    }
}
