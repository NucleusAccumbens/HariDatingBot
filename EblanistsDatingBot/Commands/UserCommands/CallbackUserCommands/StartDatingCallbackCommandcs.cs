using Application.Photos.Interfaces;
using Application.Requests.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class StartDatingCallbackCommandcs : BaseCallbackCommand
{
    private ChatRequestMessage _chatRequestMessage;
    
    private readonly string _allert = "there are no photos in this profile";

    private readonly string _usernameIsNull = 
        "there is no username in your telegram profile, the person will not be able to contact you. " +
        "add a username to send a chat request";

    private readonly string _noProfileMessage =
        "you have not created a profile yet. click /start to register";


    private readonly InlineKeyboardMarkup _keyboardMarkup = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 hide photos", callbackData: $"xHide")
        },
    });

    private readonly PhotoForDatingMessage _photoForDatingMessage = new();

    private StartDatingMessage _startDatingMessage;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetPhotosQuery _getPhotosQuery;

    private readonly ICreateRequestCommand _createRequestCommand;

    private readonly ICheckDatingUserIsBlockedQuery _checkDatingUserIsBlockedQuery;

    private readonly ICheckUsernameIsValidQuery _checkUsernameIsValidQuery;

    private readonly IUpdateTlgUserCommand _updateTlgUserCommand;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public StartDatingCallbackCommandcs(IGetDatingUserQuery datingUserQuery, IMemoryCachService memoryCachService,
        IGetPhotosQuery getPhotosQuery, ICreateRequestCommand createRequestCommand, ICheckDatingUserIsBlockedQuery checkDatingUserIsBlockedQuery,
        ICheckUsernameIsValidQuery checkUsernameIsValidQuery, IUpdateTlgUserCommand updateTlgUserCommand, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _getDatingUserQuery = datingUserQuery;
        _memoryCachService = memoryCachService;
        _getPhotosQuery = getPhotosQuery;
        _createRequestCommand = createRequestCommand;
        _checkDatingUserIsBlockedQuery = checkDatingUserIsBlockedQuery;
        _checkUsernameIsValidQuery = checkUsernameIsValidQuery;
        _updateTlgUserCommand = updateTlgUserCommand;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override char CallbackDataCode => 'w';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null && update.CallbackQuery.Data != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string data = update.CallbackQuery.Data;

            string callbackId = update.CallbackQuery.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.ShowAllert(callbackId, client, _noProfileMessage);

                return;
            }

            var users = await _getDatingUserQuery
                .GetAllDatingUsersAsync(chatId);

            try
            {
                if (data == "wStartDating")
                {
                    _memoryCachService.SetMemoryCach(chatId, users.Adapt<List<DatingUserDto>>());

                    _startDatingMessage = new(users[users.Count - 1].Adapt<DatingUserDto>(), 
                        users[users.Count - 1].ChatId);

                    await _startDatingMessage.EditMessage(chatId, messageId, client);

                    return;
                }
                if (data.Contains("wViewPhotos"))
                {
                    var photos = await _getPhotosQuery.GetUserPhotosAsync(GetUserChatId(data));                    

                    if (photos != null && photos.Count > 0)
                    {
                        if (photos.Count == 1)
                        {
                            await MessageService
                                .SendMessage(chatId, client, String.Empty, photos[0].PathToPhoto, _keyboardMarkup);

                            return;
                        }
                        
                        _memoryCachService.SetMemoryCach(chatId, photos);

                        await _photoForDatingMessage
                            .SendPhoto(chatId, client, photos[photos.Count - 1].PathToPhoto);
                    }

                    else await MessageService.ShowAllert(callbackId, client, _allert);

                    return;
                }
                if (data == "wNext")
                {
                    var user = _memoryCachService.GetCurrentUserFromMemoryCach(chatId);

                    _startDatingMessage = new(user, user.ChatId);

                    await _startDatingMessage.EditMessage(chatId, messageId, client);

                    return;
                }
                if (data.Contains("wRequestAChat"))
                {
                    string? username = update.CallbackQuery.Message.Chat.Username;

                    var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);

                    var chatIdOfCompletedRequests = _memoryCachService
                        .GetChatIdOfCompletedRequestsFromMemoryCach(chatId);

                    bool isBlocked = await _checkDatingUserIsBlockedQuery
                        .CheckDatingUserIsBlockedAsync(chatId, GetUserChatIdToRequest(data));

                    if (isBlocked == true)
                    {
                        await MessageService.ShowAllert(callbackId, client,
                            "person has blocked you. you can't send a chat request");

                        return;
                    }

                    if (username == null)
                    {
                        await MessageService.ShowAllert(callbackId, client, _usernameIsNull);

                        return;
                    }

                    bool usernameIsvalid = await _checkUsernameIsValidQuery.CheckUsernameIsValidAsync(chatId, username);

                    if (usernameIsvalid == false) await _updateTlgUserCommand.UpdateTlgUsernameAsync(chatId, username);

                    if (user != null && chatIdOfCompletedRequests == null || 
                        user != null && chatIdOfCompletedRequests != null && !chatIdOfCompletedRequests.Contains(GetUserChatIdToRequest(data)))
                    {
                        bool canRequested = _memoryCachService.SetRequestCountInMemoryCach(chatId);

                        if (canRequested == true)
                        {
                            _chatRequestMessage = new(user.Adapt<DatingUserDto>(), false);

                            var request = await CreateRequest(GetUserChatIdToRequest(data), user);

                            await _chatRequestMessage.SendMessage(GetUserChatIdToRequest(data), client);

                            _memoryCachService.SetMemoryCach(chatId, GetUserChatIdToRequest(data));

                            await MessageService.ShowAllert(callbackId, client, "application sent");
                        }
                    }

                    else await MessageService.ShowAllert(callbackId, client, 
                        "you cannot send more than one request to this person in 24 hours");
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private static long GetUserChatId(string data)
    {
        return Convert.ToInt64(data[11..]);
    }

    private static long GetUserChatIdToRequest(string data)
    {
        return Convert.ToInt64(data[13..]);
    }

    private async Task<Request> CreateRequest(long chatId, DatingUser user)
    {
        var request = new Request() { ChatId = chatId, DatingUserId = user.Id, DatingUser = user };

        await _createRequestCommand.CreateRequestAsync(request);

        return request;
    }
}
