using EblanistsDatingBot.Common.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;
using System;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands.AnswersCallbackCommands;

public class HeelsCallbackCommand : BaseCallbackCommand
{
    private readonly string _info =
        "think well and do not desire for play, attachment, and sensual pleasures, " +
        "give up decorations, be truthful, and fare alone like the single horned rhinoceros";

    private readonly string _messageText = "do you have any tattoos?";

    private readonly char _callbackCode = 'i';

    private readonly UserEthicsMessage _userEthicsMessage;

    private readonly IMemoryCachService _memoryCachService;

    public HeelsCallbackCommand(IMemoryCachService memoryCachService)
    {
        _userEthicsMessage = new(_messageText, _callbackCode);
        _memoryCachService = memoryCachService;
    }

    public override char CallbackDataCode => 'h';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            try
            {
                var user = _memoryCachService
                    .GetDatingUserDtoFromMemoryCach(chatId);

                if (update.CallbackQuery.Data == "hYes") user.IsHeelsUser = true;

                if (update.CallbackQuery.Data == "hNo") user.IsHeelsUser = false;

                if (update.CallbackQuery.Data == "hInfo")
                {
                    await MessageService.ShowAllert(callbackId, client, _info);

                    return;
                }

                _memoryCachService.SetMemoryCach(chatId, user);

                await _userEthicsMessage.EditMessage(chatId, messageId, client);
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }
}
