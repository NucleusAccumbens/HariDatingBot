using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class DeleteCallbackCommand : BaseCallbackCommand
{
    private readonly IDeleteUserCommand _deleteUserCommand;

    public DeleteCallbackCommand(IDeleteUserCommand deleteUserCommand)
    {
        _deleteUserCommand = deleteUserCommand;
    }

    public override char CallbackDataCode => '❌';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            await _deleteUserCommand.DeleteDaytingUserAsync(chatId);

            await MessageService.ShowAllert(callbackId, client, "profile deleted");

            await MessageService.DeleteMessage(chatId, messageId, client);

        }
    }
}
