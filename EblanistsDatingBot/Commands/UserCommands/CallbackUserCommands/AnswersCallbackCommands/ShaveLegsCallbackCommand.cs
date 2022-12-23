using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands.AnswersCallbackCommands;

public class ShaveLegsCallbackCommand : BaseCallbackCommand
{
    private readonly string _info =
        "the bhikkhu hearing the words of the enlightened one, becomes wise, " +
        "looks at the body as it really is, and learns it thoroughly";

    private readonly string _messageText = "do you shave your head?";

    private readonly char _callbackCode = 'l';

    private readonly UserEthicsMessage _userEthicsMessage;

    private readonly IMemoryCachService _memoryCachService;

    public ShaveLegsCallbackCommand(IMemoryCachService memoryCachService)
    {
        _userEthicsMessage = new(_messageText, _callbackCode);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'k';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var user = _memoryCachService
                    .GetDatingUserDtoFromMemoryCach(chatId);

                if (update.CallbackQuery.Data == "kYes") user.ShaveLegs = true;

                if (update.CallbackQuery.Data == "kNo") user.ShaveLegs = false;

                if (update.CallbackQuery.Data == "kInfo")
                {
                    await MessageService.ShowAllert(callbackId, client, _info);

                    return;
                }

                _memoryCachService.SetMemoryCach(chatId, user);

                await _userEthicsMessage.EditMessage(chatId, messageId, client);
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }
}
