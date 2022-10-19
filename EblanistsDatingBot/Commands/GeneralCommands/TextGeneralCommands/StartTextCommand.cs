using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.GeneralCommands.TextGeneralCommands;

public class StartTextCommand : BaseTextCommand
{
    private readonly AgeMessage _ageMessage = new();

    private readonly NavigationMessage _navigationMessage = new();

    private readonly ICheckUserIsInDbQuery _checkUserIsInDb;

    public override string Name => "/start";

    public StartTextCommand(ICheckUserIsInDbQuery checkUserIsInDb)
    {
        _checkUserIsInDb = checkUserIsInDb;
    }

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            string? username = update.Message.Chat.Username;

            bool userInDb = await _checkUserIsInDb
                .CheckUserIsInDbAsync(chatId);

            if (userInDb) await _navigationMessage.SendMessage(chatId, client);

            else await _ageMessage.SendMessage(chatId, client);
        }
    }

    
}
