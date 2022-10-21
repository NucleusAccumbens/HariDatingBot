using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class PhotoForDatingCallbackCommand : BaseCallbackCommand
{
    private readonly PhotoForDatingMessage _photoForDatingMessage = new();
    
    private readonly IMemoryCachService _memoryCachService;

    public PhotoForDatingCallbackCommand(IMemoryCachService memoryCachService)
    {
        _memoryCachService = memoryCachService;
    }
    
    public override char CallbackDataCode => 'x';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            try
            {
                if (data == "xHide")
                {
                    await MessageService.DeleteMessage(chatId, messageId, client);

                    return;
                }
                if (data.Contains("xBack"))
                {
                    var photo = _memoryCachService
                        .GetCurrentPhotoToGoBackFromMemoryCach(chatId);

                    await _photoForDatingMessage
                        .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                    return;
                }
                if (data == "xNext")
                {
                    var photo = _memoryCachService.GetCurrentPhotoFromMemoryCach(chatId);

                    await _photoForDatingMessage
                        .EditMediaMessage(chatId, messageId, client, photo.PathToPhoto);

                    return;
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }
}
