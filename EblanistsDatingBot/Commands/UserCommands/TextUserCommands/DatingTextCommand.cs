using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class DatingTextCommand : BaseTextCommand
{
    private readonly DatingMessage _datingMessage = new(true);

    private readonly string _noProfileMessage =
        "you haven't created a profile yet.  click /start to register";

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public DatingTextCommand(ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/dating";

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

            await _datingMessage.SendMessage(chatId, client);

        }
    }
}
