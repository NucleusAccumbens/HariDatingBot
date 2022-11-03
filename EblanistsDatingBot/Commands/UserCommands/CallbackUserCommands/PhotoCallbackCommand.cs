using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class PhotoCallbackCommand : BaseCallbackCommand
{
    private readonly AddPhotoMessage _addPhotoMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    public PhotoCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 's';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            await _addPhotoMessage.EditMessage(chatId, messageId, client);

            if (update.CallbackQuery.Data == "sFullBodySide")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoFullBodySide", messageId);

                return;
            }
            if (update.CallbackQuery.Data == "sFullBodyFront")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoFullBodyFront", messageId);

                return;
            }
            if (update.CallbackQuery.Data == "sFullBodyBack")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoFullBodyBack", messageId);

                return;
            }
            if (update.CallbackQuery.Data == "sPalmBack")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoPalmBack", messageId);

                return;
            }
            if (update.CallbackQuery.Data == "sPalms")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoPalms", messageId);

                return;
            }
            if (update.CallbackQuery.Data == "sFeetOnTop")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoFeetOnTop", messageId);

                return;
            }
            if (update.CallbackQuery.Data == "sFeet")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoFeet", messageId);
            }
            if (update.CallbackQuery.Data == "sCrotch")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoCrotch", messageId);
            }
            if (update.CallbackQuery.Data == "sUnderarm")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoUnderarm", messageId);
            }
            if (update.CallbackQuery.Data == "sOthers")
            {
                _memoryCachService.SetMemoryCach(chatId, "addPhotoOthers", messageId);
            }
        }
    }
}
