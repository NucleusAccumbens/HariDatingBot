using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class ChatingTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    public ChatingTextCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override string Name => "chating";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            string text = update.Message.Text;

            long chatingUserChatId = _memoryCachService
                .GetInterlocutorChatIdFromMemoryCach(chatId);

            await MessageService.SendMessage(chatingUserChatId, client, text, null);
        }
    }
}
