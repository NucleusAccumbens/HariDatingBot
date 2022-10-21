using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands;

public class ProfileTextCommand : BaseTextCommand
{

    private ProfileMessage _profileMessage;
    
    private readonly IGetDatingUserQuery _getDatingUserQuery;

    public ProfileTextCommand(IGetDatingUserQuery getDatingUserQuery)
    {
        _getDatingUserQuery = getDatingUserQuery;
    }

    public override string Name => "/profile";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);

            if (user != null)
            {
                _profileMessage = new(user.Adapt<DatingUserDto>(), true);

                await _profileMessage.SendMessage(chatId, client);
            }           
        }
    }
}
