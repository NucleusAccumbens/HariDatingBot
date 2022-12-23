using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.GeneralMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class HelpTextCommand : BaseTextCommand
{
    private readonly HelpMessage _helpMessage = new();

    private readonly string _noProfileMessage =
        "you haven't created a profile yet.  click /start to register";

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public HelpTextCommand(ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/help";

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

            await _helpMessage.SendMessage(chatId, client);
        }
    }
}
