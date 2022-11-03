using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class RequestsTextCommand : BaseTextCommand
{
    private readonly string _noProfileMessage =
    "you have not created a profile yet. click /start to register";

    private readonly IGetDatingUserRequestsQuery _getDatingUserRequestsQuery;

    private readonly ICheckDatingUserIsBlockedQuery _checkDatingUserIsBlockedQuery;

    private ChatRequestMessage _requestAChatMessage;

    private readonly string _text = "you don't have any chat requests yet";

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;


    public RequestsTextCommand(IGetDatingUserRequestsQuery getDatingUserRequestsQuery, 
        ICheckDatingUserIsBlockedQuery checkDatingUserIsBlockedQuery, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _getDatingUserRequestsQuery = getDatingUserRequestsQuery;
        _checkDatingUserIsBlockedQuery = checkDatingUserIsBlockedQuery;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/requests";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
            {
                await MessageService.SendMessage(chatId, client, _noProfileMessage, null);

                return;
            }

            var requests = await _getDatingUserRequestsQuery
                .GetRequestsAsync(chatId);

            if (requests == null || requests.Count == 0)
            {
                await MessageService.SendMessage(chatId, client, _text, null);
            }

            else
            {
                foreach (var request in requests)
                {
                    if (request.DatingUser != null)
                    {
                        bool isBlocked = await _checkDatingUserIsBlockedQuery
                            .CheckDatingUserIsBlockedAsync(request.DatingUser.ChatId, chatId);

                        _requestAChatMessage =
                            new(request.DatingUser.Adapt<DatingUserDto>(), isBlocked);

                        await _requestAChatMessage.SendMessage(chatId, client);
                    }
                }
            }
        }
    }
}
