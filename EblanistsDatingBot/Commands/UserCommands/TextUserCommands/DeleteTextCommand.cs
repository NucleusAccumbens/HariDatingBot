using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class DeleteTextCommand : BaseTextCommand
{
    private readonly string _noProfileMessage =
        "you haven't created a profile yet.  click /start to register";

    private readonly DeleteMessage _deleteMessage = new();

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public DeleteTextCommand(ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/delete_profile";

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

            await _deleteMessage.SendMessage(chatId, client);
        }
    }
}
