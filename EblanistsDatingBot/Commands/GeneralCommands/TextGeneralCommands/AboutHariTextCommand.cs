using EblanistsDatingBot.Messages.GeneralMessages;

namespace EblanistsDatingBot.Commands.GeneralCommands.TextGeneralCommands;

public class AboutHariTextCommand : BaseTextCommand
{
    private readonly AboutHariMessage _aboutHariMessage = new(); 
    
    public override string Name => "/about_hari";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null)
        {
            long chatId = update.Message.Chat.Id;

            await _aboutHariMessage.SendMessage(chatId, client);
        }
    }
}
