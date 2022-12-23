using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class StiTestCallbackCommand : BaseCallbackCommand
{
    public override char CallbackDataCode => 'q';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            await MessageService.ShowAllert(callbackId, client, "this feature is not yet available");
        }
    }
}
