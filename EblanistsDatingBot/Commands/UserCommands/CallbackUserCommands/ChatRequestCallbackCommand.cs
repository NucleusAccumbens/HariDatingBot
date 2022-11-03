using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class ChatRequestCallbackCommand : BaseCallbackCommand
{
    private ChatRequestMessage _chatRequestMessage;

    private AcceptRequestMessage _acceptRequestMessage;

    private RequestApprovalNotificationMessage _requestApprovalNotificationMessage;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    private readonly IBlockDatingUserCommand _blockDatingUserCommand;

    private readonly IGetUsernameTlgUserQuery _getUsernameTlgUserQuery;

    private readonly ICheckDatingUserIsBlockedQuery _checkDatingUserIsBlockedQuery;


    public ChatRequestCallbackCommand(IGetDatingUserQuery getDatingUserQuery, 
        IBlockDatingUserCommand blockDatingUserCommand, IGetUsernameTlgUserQuery getUsernameTlgUserQuery, 
        ICheckDatingUserIsBlockedQuery checkDatingUserIsBlockedQuery)
    {
        _getDatingUserQuery = getDatingUserQuery;
        _blockDatingUserCommand = blockDatingUserCommand;
        _getUsernameTlgUserQuery = getUsernameTlgUserQuery;
        _checkDatingUserIsBlockedQuery = checkDatingUserIsBlockedQuery;
    }

    public override char CallbackDataCode => 'y';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string? username = update.CallbackQuery.Message.Chat.Username;

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
                        
                        _chatRequestMessage = new(blokingUser.Adapt<DatingUserDto>(), true);

                        await _chatRequestMessage.EditMessage(chatId, messageId, client);

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

                        _chatRequestMessage = new(user.Adapt<DatingUserDto>(), false);

                        await _chatRequestMessage.EditMessage(chatId, messageId, client);

                        await MessageService.ShowAllert(callbackId, client,
                            "persona unlocked");
                    }

                    return;
                }
                if (data.Contains("yAccept"))
                {
                    if (username == null)
                    {
                        await MessageService.ShowAllert(callbackId, client,
                            "there is no username in your telegram profile. " +
                            "to accept a chat request, add a username to your profile");

                        return;
                    }
                    
                    long interlocutorChatId = GetChatIdForUnlockUser(data);

                    string? interlocutorUsername = await _getUsernameTlgUserQuery
                        .GetUsernameAsync(interlocutorChatId);

                    if (interlocutorUsername != null)
                    {
                        _acceptRequestMessage = new(interlocutorUsername, interlocutorChatId);

                        _requestApprovalNotificationMessage = new(username);

                        await _acceptRequestMessage.EditMessage(chatId, messageId, client);

                        await _requestApprovalNotificationMessage.SendMessage(interlocutorChatId, client);
                    }

                    return;
                }
                if (data.Contains("yBack"))
                {
                    var user = await _getDatingUserQuery.GetDatingUserAsync(GetChatIdForGoBack(data));                

                    if (user != null)
                    {
                        bool isBlocked = await _checkDatingUserIsBlockedQuery
                            .CheckDatingUserIsBlockedAsync(user.ChatId, chatId);

                        _chatRequestMessage = new(user.Adapt<DatingUserDto>(), isBlocked);

                        await _chatRequestMessage.EditMessage(chatId, messageId, client);
                    }
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private static long GetChatIdForBlockUser(string data)
    {
        return Convert.ToInt64(data[6..]);
    }

    private static long GetChatIdForUnlockUser(string data)
    {
        return Convert.ToInt64(data[7..]);
    }

    private static long GetChatIdForGoBack(string data)
    {
        return Convert.ToInt64(data[5..]);
    }
}
