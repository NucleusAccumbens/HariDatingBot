using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class RequestsTextCommand : BaseTextCommand
{
    private readonly IGetDatingUserRequestsQuery _getDatingUserRequestsQuery;

    private RequestAChatMessage _requestAChatMessage;


    public RequestsTextCommand(IGetDatingUserRequestsQuery getDatingUserRequestsQuery)
    {
        _getDatingUserRequestsQuery = getDatingUserRequestsQuery;
    }

    public override string Name => "/requests";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            var requests = await _getDatingUserRequestsQuery
                .GetRequestsAsync(chatId);

            foreach (var request in requests)
            {               
                if (request.DatingUser != null)
                {
                    _requestAChatMessage =
                        new(request.DatingUser.Adapt<DatingUserDto>(), 
                        request.DatingUser.ChatId, request.IsBlocked);

                    await _requestAChatMessage.SendMessage(chatId, client);
                }
            }
        }
    }
}
