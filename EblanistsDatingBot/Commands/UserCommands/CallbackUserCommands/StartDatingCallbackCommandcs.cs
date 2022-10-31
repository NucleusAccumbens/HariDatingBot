using Application.BlockedUsers.Queries;
using Application.Photos.Interfaces;
using Application.Requests.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class StartDatingCallbackCommandcs : BaseCallbackCommand
{
    private RequestAChatMessage _chatMessage;
    
    private readonly string _allert = "there are no photos in this profile";

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

    public StartDatingCallbackCommandcs(IGetDatingUserQuery datingUserQuery, IMemoryCachService memoryCachService,
        IGetPhotosQuery getPhotosQuery, ICreateRequestCommand createRequestCommand, ICheckDatingUserIsBlockedQuery checkDatingUserIsBlockedQuery)
    {
        _getDatingUserQuery = datingUserQuery;
        _memoryCachService = memoryCachService;
        _getPhotosQuery = getPhotosQuery;
        _createRequestCommand = createRequestCommand;
        _checkDatingUserIsBlockedQuery = checkDatingUserIsBlockedQuery;
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

            var users = await _getDatingUserQuery
                .GetAllDatingUsersAsync();

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
                    var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);

                    var chatIdOfCompletedRequests = _memoryCachService
                        .GetChatIdOfCompletedRequestsFromMemoryCach(GetUserChatIdToRequest(data));

                    bool isBlocked = await _checkDatingUserIsBlockedQuery
                        .CheckDatingUserIsBlockedAsync(chatId, GetUserChatIdToRequest(data));

                    if (isBlocked == true)
                    {
                        await MessageService.ShowAllert(callbackId, client,
                            "person has blocked you. you can't send a chat request");

                        return;
                    }

                    if (user != null && chatIdOfCompletedRequests == null || 
                        user != null && chatIdOfCompletedRequests != null && !chatIdOfCompletedRequests.Contains(user.ChatId))
                    {
                        bool canRequested = _memoryCachService.SetRequestCountInMemoryCach(chatId);

                        if (canRequested == true)
                        {
                            _chatMessage = new(user.Adapt<DatingUserDto>(), chatId, false);

                            var request = await CreateRequest(GetUserChatIdToRequest(data), user);

                            await _chatMessage.SendMessage(GetUserChatIdToRequest(data), client);

                            _memoryCachService.SetMemoryCach(chatId, user.ChatId);

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
