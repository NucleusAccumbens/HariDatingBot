using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands.AnswersCallbackCommands;

public class ExistenceCallbackCommand : BaseCallbackCommand
{
    private readonly string _info =
        "if someone does not see any essence in being, " +
        "seeing it as a search for wood-apple flowers, " +
        "he gives up this and the other world, " +
        "like the snake that discards the decayed skin";

    private readonly string _messageText = "do you shave your legs?";

    private readonly char _callbackCode = 'k';

    private readonly UserEthicsMessage _userEthicsMessage;

    private readonly IMemoryCachService _memoryCachService;

    public ExistenceCallbackCommand(IMemoryCachService memoryCachService)
    {
        _userEthicsMessage = new(_messageText, _callbackCode);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'j';

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

                if (update.CallbackQuery.Data == "jYes") user.IsExistLover = true;

                if (update.CallbackQuery.Data == "jNo") user.IsExistLover = false;

                if (update.CallbackQuery.Data == "jInfo")
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
