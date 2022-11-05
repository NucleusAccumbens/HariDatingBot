using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class DeleteCallbackCommand : BaseCallbackCommand
{
    private readonly string _noProfileMessage =
    "you haven't created a profile yet. click /start to register";

    private readonly IDeleteUserCommand _deleteUserCommand;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public DeleteCallbackCommand(IDeleteUserCommand deleteUserCommand, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _deleteUserCommand = deleteUserCommand;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override char CallbackDataCode => '❌';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.ShowAllert(callbackId, client, _noProfileMessage);

                return;
            }

            await _deleteUserCommand.DeleteDaytingUserAsync(chatId);

            await MessageService.ShowAllert(callbackId, client, "profile deleted");
        }
    }
}
