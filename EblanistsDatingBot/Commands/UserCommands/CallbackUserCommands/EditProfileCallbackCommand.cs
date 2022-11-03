using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class EditProfileCallbackCommand : BaseCallbackCommand
{
    private readonly string _noProfileMessage =
        "you have not created a profile yet. click /start to register";

    private readonly EditProfileMessage _editProfileMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public EditProfileCallbackCommand(IMemoryCachService memoryCachService, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _memoryCachService = memoryCachService;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override char CallbackDataCode => 'p';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.ShowAllert(callbackId, client, _noProfileMessage);

                return;
            }

            _memoryCachService.SetMemoryCach(chatId, String.Empty, 0);

            await _editProfileMessage.EditMessage(chatId, messageId, client);
        }
    }
}
