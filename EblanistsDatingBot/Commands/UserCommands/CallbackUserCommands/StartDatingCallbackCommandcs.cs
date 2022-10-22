using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class StartDatingCallbackCommandcs : BaseCallbackCommand
{
    private RequestAChatMessage _chatMessage;
    
    private readonly string _allert = "there are no photos in this profile";

    private readonly PhotoForDatingMessage _photoForDatingMessage = new();

    private StartDatingMessage _startDatingMessage;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetPhotosQuery _getPhotosQuery;

    private readonly IUpdateDatingUserCommand _updateDatingUserCommand;

    public StartDatingCallbackCommandcs(IGetDatingUserQuery datingUserQuery, IMemoryCachService memoryCachService,
        IGetPhotosQuery getPhotosQuery, IUpdateDatingUserCommand updateDatingUserCommand)
    {
        _getDatingUserQuery = datingUserQuery;
        _memoryCachService = memoryCachService;
        _getPhotosQuery = getPhotosQuery;
        _updateDatingUserCommand = updateDatingUserCommand;
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
                        .GetChatIdOfCompletedRequestsFromMemoryCach(chatId);

                    if (user != null && chatIdOfCompletedRequests == null || 
                        user != null && chatIdOfCompletedRequests != null && !chatIdOfCompletedRequests.Contains(user.ChatId))
                    {
                        _chatMessage = new(user.Adapt<DatingUserDto>(), chatId, false);

                        var request = new Request() { ChatId = chatId };

                        await _updateDatingUserCommand.AddRequestAsync(GetUserChatIdToWriteMessage(data), request);

                        await _chatMessage.SendMessage(GetUserChatIdToWriteMessage(data), client);

                        _memoryCachService.SetMemoryCach(chatId, user.ChatId);

                        await MessageService.ShowAllert(callbackId, client, "application sent");
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

    private static long GetUserChatIdToWriteMessage(string data)
    {
        return Convert.ToInt64(data[13..]);
    }
}
