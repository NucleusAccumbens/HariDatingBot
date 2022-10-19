using EblanistsDatingBot.Common.Interfaces;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class RegisterCallbackCommand : BaseCallbackCommand
{
    private readonly string _messageText = "are you a vegan?";

    private readonly char _callbackCode = 'a';
    
    private readonly UserEthicsMessage _userEthicsMessage;

    private readonly IMemoryCachService _memoryCachService;

    public RegisterCallbackCommand(IMemoryCachService memoryCachService)
    {
        _userEthicsMessage = new(_messageText, _callbackCode);
        _memoryCachService = memoryCachService;
    }
    
    public override char CallbackDataCode => '.';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            _memoryCachService
                .SetMemoryCach(chatId, new DatingUserDto() { ChatId = chatId });

            await _userEthicsMessage.EditMessage(chatId, messageId, client);
        }
    }
}
