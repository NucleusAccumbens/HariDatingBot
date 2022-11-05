using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class EditProfileParamsCallbackCommand : BaseCallbackCommand
{
    private readonly string _noProfileMessage =
        "you haven't created a profile yet. click /start to register";

    private readonly PhotoMessage _photoMessage = new();
    
    private readonly AboutMessage _aboutMessage = new();

    private readonly EditAnswersMessage _editAnswersMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public EditProfileParamsCallbackCommand(IMemoryCachService memoryCachService, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _memoryCachService = memoryCachService;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override char CallbackDataCode => 'r';

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
