using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class EditProfileCallbackCommand : BaseCallbackCommand
{
    private readonly EditProfileMessage _editProfileMessage = new();

    private readonly IMemoryCachService _memoryCachService;
   
    public EditProfileCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'p';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            _memoryCachService.SetMemoryCach(chatId, String.Empty, 0);

            await _editProfileMessage.EditMessage(chatId, messageId, client);
        }
    }
}
