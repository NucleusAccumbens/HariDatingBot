using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands.AnswersCallbackCommands;

public class ChildfreeCallbackCommand : BaseCallbackCommand
{
    private readonly string _info =
        "childfree is a refusal to reproduce\n" +
        "childfree can have natural and adopted children\n\n" +
        "i recommend the book \"better never be: the harm of arising\" by david benatar";

    private readonly string _messageText = "do you believe in anything?";

    private readonly char _callbackCode = 'c';

    private readonly UserEthicsMessage _userEthicsMessage;

    private readonly IMemoryCachService _memoryCachService;

    public ChildfreeCallbackCommand(IMemoryCachService memoryCachService)
    {
        _userEthicsMessage = new(_messageText, _callbackCode);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'b';

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

                if (update.CallbackQuery.Data == "bYes") user.IsChildfree = true;

                if (update.CallbackQuery.Data == "bNo") user.IsChildfree = false;

                if (update.CallbackQuery.Data == "bInfo")
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
