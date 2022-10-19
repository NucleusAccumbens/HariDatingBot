using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Common.Abstractions;

public abstract class BaseMessage
{
    public abstract string MessageText { get; }

    public abstract InlineKeyboardMarkup InlineKeyboardMarkup { get; }

    public virtual async Task SendMessage(long chatId, ITelegramBotClient client)
    {
        await MessageService
            .SendMessage(chatId, client, MessageText, InlineKeyboardMarkup);
    }

    public virtual async Task SendPhoto(long chatId, ITelegramBotClient client, string path)
    {
        await MessageService
            .SendMessage(chatId, client, MessageText, path, InlineKeyboardMarkup);
    }

    public virtual async Task EditMessage(long chatId, int messageId, ITelegramBotClient client)
    {
        await MessageService
            .EditMessage(chatId, messageId, client, MessageText, InlineKeyboardMarkup);
    }

}
