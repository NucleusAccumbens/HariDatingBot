using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages;
using EblanistsDatingBot.Messages.GeneralMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class AgeCallbackCommand : BaseCallbackCommand
{
    private readonly RegisterMessage _registerMessage = new();

    public override char CallbackDataCode => '!';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data == "!Yes")
            {
                await _registerMessage.EditMessage(chatId, messageId, client);

                return;
            }

            else await MessageService.ShowAllert(callbackId, client,
                "you cannot use hari until you are 18 years old");
        }
    }
}
