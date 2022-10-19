using Application.Photos.Interfaces;
using Domain.Enums;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.AdminMessage;
using EblanistsDatingBot.Messages.UserMessages;
using Telegram.Bot.Types;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class PhotoTextCommand : BaseTextCommand
{
    private readonly IMemoryCachService _memoryCachService;

    private readonly IGetAdminsChatIdQuery _getAdminsChatIdQuery;

    private readonly ICreatePhotoCommand _createPhotoCommand;

    private readonly PhotoSubmittedForVerificationMessage _photoSubmittedForVerificationMessage = new();

    private VerifidePhotoMessage _verifidePhotoMessage;

    public PhotoTextCommand(IMemoryCachService memoryCachService, 
        IGetAdminsChatIdQuery getAdminsChatIdQuery, ICreatePhotoCommand createPhotoCommand)
    {
        _memoryCachService = memoryCachService;
        _getAdminsChatIdQuery = getAdminsChatIdQuery;
        _createPhotoCommand = createPhotoCommand;
    }

    public override string Name => "addPhoto";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Photo != null)
        {
            long chatId = update.Message.Chat.Id;

            int photoMessageId = update.Message.MessageId;

            string path = update.Message.Photo[2].FileId;

            try
            {
                string commandState = _memoryCachService.GetCommandStateFromMemoryCach(chatId);
                
                int messageId = _memoryCachService.GetMessageIdFromMemoryCach(chatId);

                await MessageService.DeleteMessage(chatId, photoMessageId, client);

                _memoryCachService.SetMemoryCach(chatId, String.Empty, 0);

                await _photoSubmittedForVerificationMessage.EditMessage(chatId, messageId, client);

                if (commandState == "addPhotoFullBodyFront")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.FullBodyFront, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.FullBodyFront, photoId);

                    return;
                }
                if (commandState == "addPhotoFullBodySide")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.FullBodySide, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.FullBodySide, photoId);

                    return;
                }
                if (commandState == "addPhotoFullBodyBack")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.FullBodyBack, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.FullBodyBack, photoId);

                    return;
                }
                if (commandState == "addPhotoPortrait")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.Portrait, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.Portrait, photoId);

                    return;
                }
                if (commandState == "addPhotoFeet")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.Feet, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.Feet, photoId);

                    return;
                }
                if (commandState == "addPhotoFeetOnTop")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.FeetOnTop, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.FeetOnTop, photoId);

                    return;
                }
                if (commandState == "addPhotoPalms")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.Palm, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.Palm, photoId);

                    return;
                }
                if (commandState == "addPhotoPalmBack")
                {
                    long photoId = await AddPhotoToDatingUser(chatId, BodyParts.PalmBack, path);

                    await SendVerifideMessageToAdmins(client, path, BodyParts.PalmBack, photoId);
                }
            }
            catch (MemoryCachException ex)
            {
                await ex.SendExceptionMessage(chatId, client);
            }
        }
    }

    private async Task<long> AddPhotoToDatingUser(long chatId, BodyParts bodyParts, string path)
    {
        var photo = new PhotoDto()
        {
            ChatId = chatId,
            BodyPart = bodyParts,
            PathToPhoto = path,
        };

        return await _createPhotoCommand.CreatePhotoAsync(photo.Adapt<Photo>());
    }

    private async Task SendVerifideMessageToAdmins(ITelegramBotClient client, string path, 
        BodyParts bodyPart, long photoId)
    {
        _verifidePhotoMessage = new(bodyPart, photoId);
        
        var chatIds = await _getAdminsChatIdQuery.GetAdminsChatIdAsync();

        foreach (var chatId in chatIds)
        {
            await _verifidePhotoMessage.SendPhoto(chatId, client, path);
        }
    }
}

