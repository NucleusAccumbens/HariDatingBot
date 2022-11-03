using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class FeedbackTextCommand : BaseTextCommand
{
    private readonly FeedbackMessage _feedbackMessage = new();

    private readonly string _noProfileMessage =
    "you have not created a profile yet. click /start to register";

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public FeedbackTextCommand(ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/feedback";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.SendMessage(chatId, client, _noProfileMessage, null);

                return;
            }

            await _feedbackMessage.SendMessage(chatId, client);
        }
    }
}
