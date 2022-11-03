using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class DatingCallbackCommand : BaseCallbackCommand
{
    private readonly string _noProfileMessage =
    "you have not created a profile yet. click /start to register";

    private readonly DatingMessage _datingMessage = new(false);

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public DatingCallbackCommand(ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override char CallbackDataCode => 'n';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.ShowAllert(callbackId, client, _noProfileMessage);

                return;
            }

            await _datingMessage.EditMessage(chatId, messageId, client);

        }
    }
}
