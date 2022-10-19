using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class EditProfileParamsCallbackCommand : BaseCallbackCommand
{
    private readonly PhotoMessage _photoMessage = new();
    
    private readonly AboutMessage _aboutMessage = new();

    private readonly EditAnswersMessage _editAnswersMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    public EditProfileParamsCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }
    
    public override char CallbackDataCode => 'r';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            if (update.CallbackQuery.Data == "rAddAbout")
            {
                _memoryCachService.SetMemoryCach(chatId, "addAbout", messageId);
                
                await _aboutMessage
                    .EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "rEditAnswers")
            {
                await _editAnswersMessage.EditMessage(chatId, messageId, client);

                return;
            }
            if (update.CallbackQuery.Data == "rAddPhotos")
            {
                await _photoMessage.EditMessage(chatId, messageId, client);
            }
        }
    }
}
