using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class RequestAChatCallbackCommand : BaseCallbackCommand
{
    private RequestAChatMessage _requestAChatMessage;

    private readonly ChatMessage _chatMessage = new();
    
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    private readonly IBlockDatingUserCommand _blockDatingUserCommand;

    public RequestAChatCallbackCommand(IMemoryCachService memoryCachService, 
        IGetDatingUserQuery getDatingUserQuery, IBlockDatingUserCommand blockDatingUserCommand)
    {
        _memoryCachService = memoryCachService;
        _getDatingUserQuery = getDatingUserQuery;
        _blockDatingUserCommand = blockDatingUserCommand;
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
                        await _blockDatingUserCommand.BlockUserAsync(chatId, blokingUser.ChatId);
                        
                        _requestAChatMessage = new(blokingUser.Adapt<DatingUserDto>(), blokingUser.ChatId, true);

                        await _requestAChatMessage.EditMessage(chatId, messageId, client);

                        await MessageService.ShowAllert(callbackId, client,
                            "the person is blocked and will not be able to send a repeated request for a dialogue");
                    }

                    return;
                }
                if (data.Contains("yUnlock"))
                {
                    var user = await _getDatingUserQuery.GetDatingUserAsync(GetChatIdForUnlockUser(data));

                    if (user != null)
                    {
                        await _blockDatingUserCommand.UnlockUserAsync(chatId, user.ChatId);

                        _requestAChatMessage = new(user.Adapt<DatingUserDto>(), user.ChatId, false);

                        await _requestAChatMessage.EditMessage(chatId, messageId, client);

                        await MessageService.ShowAllert(callbackId, client,
                            "persona unlocked");
                    }

                    return;
                }
                if (data.Contains("ySendAMessage"))
                {
                    _memoryCachService
                        .SetInterlocutorChatIdInMemoryCach(chatId, GetChatIdForSendMessage(data));

                    await _chatMessage.EditMessage(chatId, messageId, client);
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

    private long GetChatIdForSendMessage(string data)
    {
        return Convert.ToInt64(data[13..]);
    }
}
