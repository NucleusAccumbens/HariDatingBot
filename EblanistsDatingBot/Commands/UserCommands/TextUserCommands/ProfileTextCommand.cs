using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands;

public class ProfileTextCommand : BaseTextCommand
{
    private readonly string _noProfileMessage =
        "you haven't created a profile yet.  click /start to register";

    private ProfileMessage _profileMessage;
    
    private readonly IGetDatingUserQuery _getDatingUserQuery;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public ProfileTextCommand(IGetDatingUserQuery getDatingUserQuery, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _getDatingUserQuery = getDatingUserQuery;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/profile";

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

            var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);

            if (user != null)
            {
                _profileMessage = new(user.Adapt<DatingUserDto>(), true);

                await _profileMessage.SendMessage(chatId, client);
            }           
        }
    }
}
