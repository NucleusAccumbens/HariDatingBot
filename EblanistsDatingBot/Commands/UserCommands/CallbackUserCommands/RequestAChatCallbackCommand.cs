using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class RequestAChatCallbackCommand : BaseCallbackCommand
{
    private RequestAChatMessage _requestAChatMessage;
    
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetDatingUserQuery _getDatingUserQuery;


    public RequestAChatCallbackCommand(IMemoryCachService memoryCachService, 
        IGetDatingUserQuery getDatingUserQuery)
    {
        _memoryCachService = memoryCachService;
        _getDatingUserQuery = getDatingUserQuery;
    }

    public override char CallbackDataCode => 'y';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            string callbackId = update.CallbackQuery.Id;


            try
            {
                if (data.Contains("yBlock"))
                {
                    var blokingUser = await _getDatingUserQuery.GetDatingUserAsync(GetChatIdForBlockUser(data));

                    if (blokingUser != null)
                    {

                        _requestAChatMessage = new(blokingUser.Adapt<DatingUserDto>(), blokingUser.ChatId, true);

                        await _requestAChatMessage.EditMessage(chatId, messageId, client);

                        await MessageService.ShowAllert(callbackId, client,
                            "the person is blocked and will not see your profile in the search " +
                            "and will not be able to send a second request for dialogue");
                    }

                    return;
                }
                if (data.Contains("yUnlock"))
                {
                    var user = await _getDatingUserQuery.GetDatingUserAsync(GetChatIdForUnlockUser(data));

                    if (user != null)
                    {
                        _requestAChatMessage = new(user.Adapt<DatingUserDto>(), user.ChatId, false);

                        await _requestAChatMessage.EditMessage(chatId, messageId, client);

                        await MessageService.ShowAllert(callbackId, client,
                            "persona unlocked");
                    }

                    return;
                }
                if (data.Contains("ySendAMessage"))
                {

                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private long GetChatIdForBlockUser(string data)
    {
        return Convert.ToInt64(data[6..]);
    }

    private long GetChatIdForUnlockUser(string data)
    {
        return Convert.ToInt64(data[7..]);
    }
}
