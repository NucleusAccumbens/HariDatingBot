using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands.AnswersCallbackCommands;

public class SacredCallbackCommand : BaseCallbackCommand
{  
    private readonly string _info =
        "cut up the bonds, like the fish that dismantle the net " +
        "and like the fire that burns non-stop fare alone like the single horned rhinoceros";

    private readonly NavigationMessage _navigationMessage = new();

    private readonly IMemoryCachService _memoryCachService;

    private readonly ICreateDatingUserCommand _createDatingUserCommand;

    private readonly ICreateTlgUserCommand _createTlgUserCommand;

    public SacredCallbackCommand(IMemoryCachService memoryCachService,
        ICreateDatingUserCommand createDatingUserCommand, ICreateTlgUserCommand createTlgUserCommand)
    {
        _memoryCachService = memoryCachService;
        _createDatingUserCommand = createDatingUserCommand;
        _createTlgUserCommand = createTlgUserCommand;
    }

    public override char CallbackDataCode => 'm';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string? username = update.CallbackQuery.Message.Chat.Username;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var user = _memoryCachService
                    .GetDatingUserDtoFromMemoryCach(chatId);

                if (update.CallbackQuery.Data == "mYes") user.HaveSacred = true;

                if (update.CallbackQuery.Data == "mNo") user.HaveSacred = false;

                if (update.CallbackQuery.Data == "mInfo")
                {
                    await MessageService.ShowAllert(callbackId, client, _info);

                    return;
                }

                await CreateTlgUser(new TlgUserDto() { ChatId = chatId, Username = username, IsAdmin = false });

                await _createDatingUserCommand
                    .CreateDatingUserAsync(user.Adapt<DatingUser>());

                await MessageService.ShowAllert(callbackId, client, "registration completed");

                await _navigationMessage.EditMessage(chatId, messageId, client);
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private async Task CreateTlgUser(TlgUserDto tlgUserDto)
    {
        await _createTlgUserCommand
            .CreateTlgUserAsync(tlgUserDto.Adapt<TlgUser>());
    }
}
