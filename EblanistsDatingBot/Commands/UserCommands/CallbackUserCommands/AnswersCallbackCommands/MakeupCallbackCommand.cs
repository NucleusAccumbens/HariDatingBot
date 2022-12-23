using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands.AnswersCallbackCommands;

public class MakeupCallbackCommand : BaseCallbackCommand
{
    private readonly string _info =
        "when the mind is imbued with wisdom it is rightly freed from the defilements, " +
        "namely, the defilements of sensuality, desire to be reborn, and ignorance";

    private readonly string _messageText = "do you wear heels?";

    private readonly char _callbackCode = 'h';

    private readonly UserEthicsMessage _userEthicsMessage;

    private readonly IMemoryCachService _memoryCachService;

    public MakeupCallbackCommand(IMemoryCachService memoryCachService)
    {
        _userEthicsMessage = new(_messageText, _callbackCode);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'g';

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

                if (update.CallbackQuery.Data == "gYes") user.IsMakeupUser = true;

                if (update.CallbackQuery.Data == "gNo") user.IsMakeupUser = false;

                if (update.CallbackQuery.Data == "gInfo")
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
