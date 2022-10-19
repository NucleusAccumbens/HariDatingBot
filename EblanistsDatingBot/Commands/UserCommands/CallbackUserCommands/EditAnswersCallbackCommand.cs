using EblanistsDatingBot.Common.Services;

namespace EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;

public class EditAnswersCallbackCommand : BaseCallbackCommand
{
    private readonly IUpdateDatingUserCommand _updateDatingUserCommand;

    public EditAnswersCallbackCommand(IUpdateDatingUserCommand updateDatingUserCommand)
    {
        _updateDatingUserCommand = updateDatingUserCommand;
    }

    public override char CallbackDataCode => 'd';

    public override async Task CallbackExecute(Update update, ITelegramBotClient client)
    {
        if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;

            int messageId = update.CallbackQuery.Message.MessageId;

            string callbackId = update.CallbackQuery.Id;

            if (update.CallbackQuery.Data == "d1")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsVegan");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d2")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsBeliever");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d3")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsChildfree");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d4")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsCosmopolitan");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d5")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsBdsmLover");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d6")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsMakeupUser");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d7")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsHeelsUser");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d8")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsTattooed");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d9")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "IsExistLover");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d10")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "ShaveLegs");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d11")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "ShaveHead");

                await ShowAllert(callbackId, client, value);

                return;
            }
            if (update.CallbackQuery.Data == "d12")
            {
                bool? value = await _updateDatingUserCommand
                    .UpdateUserEthicsAsync(chatId, "HaveSacred");

                await ShowAllert(callbackId, client, value);
            }
        }
    }

    private async Task ShowAllert(string callbackId, ITelegramBotClient client, bool? value)
    {
        string answer = String.Empty;

        if (value == true) answer = "yes";

        if (value == false) answer = "no";

        await MessageService
            .ShowAllert(callbackId, client, $"answer changed to \"{answer}\"");
    }
}
