using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class AddAboutTextCommand : BaseTextCommand
{
    private ProfileMessage _profileMessage;

    private readonly IMemoryCachService _memoryCachService;

    private readonly IUpdateDatingUserCommand _updateDatingUserCommand;

    private readonly IGetDatingUserQuery _getDatingUserQuery;

    public AddAboutTextCommand(IMemoryCachService memoryCachService, 
        IUpdateDatingUserCommand updateDatingUserCommand, IGetDatingUserQuery getDatingUserQuery)
    {
        _memoryCachService = memoryCachService;
        _updateDatingUserCommand = updateDatingUserCommand;
        _getDatingUserQuery = getDatingUserQuery;
    }
    
    public override string Name => "addAbout";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            int textMessageId = update.Message.MessageId;

            string text = update.Message.Text;

            try
            {
                int messageId = _memoryCachService.GetMessageIdFromMemoryCach(chatId);

                await MessageService.DeleteMessage(chatId, textMessageId, client);

                if (text.Length > 500) return;

                if (text.Length <= 500)
                {
                    await _updateDatingUserCommand
                        .UpdateUserAboutAsync(chatId, text);

                    var user = await _getDatingUserQuery.GetDatingUserAsync(chatId);
                    
                    if (user != null ) _profileMessage = new(user.Adapt<DatingUserDto>());

                    _memoryCachService.SetMemoryCach(chatId, String.Empty, 0);

                    await _profileMessage.EditMessage(chatId, messageId, client);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }
}
