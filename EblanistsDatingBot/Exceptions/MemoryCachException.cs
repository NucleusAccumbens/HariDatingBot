using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Exceptions;
public class MemoryCachException : Exception
{
    private readonly string _text = "Too much time has passed since the previous session, " +
         "the data entered earlier has not been saved.\n\n" +
         "To start over, select command:\n\n" +
         "/start - start work\n";

    public MemoryCachException()
        : base()
    {
    }

    public async Task SendExceptionMessage(long chatId, ITelegramBotClient client)
    {
        await MessageService.SendMessage(chatId, client, _text, null);
    }
}
