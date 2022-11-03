using Application.Photos.Interfaces;
using EblanistsDatingBot.Common.Services;
using EblanistsDatingBot.Messages.UserMessages;

namespace EblanistsDatingBot.Commands.UserCommands.TextUserCommands;

public class ViewPhotosTextCommand : BaseTextCommand
{
    private readonly string _noProfileMessage = 
        "you have not created a profile yet. click /start to register";

    private readonly string _allert = "there are no photos in your profile";

    private ViewPhotoMessage _viewPhotoMessage;

    private readonly IMemoryCachService _memoryCachService; 

    private readonly ICheckUserHasPhotosQuery _checkUserHasPhotosQuery;

    private readonly IGetPhotosQuery _getPhotosQuery;

    private readonly ICheckUserIsInDbQuery _checkUserIsInDbQuery;

    public ViewPhotosTextCommand(IGetPhotosQuery getPhotosQuery, 
        ICheckUserHasPhotosQuery checkUserHasPhotosQuery, IMemoryCachService memoryCachService, ICheckUserIsInDbQuery checkUserIsInDbQuery)
    {
        _getPhotosQuery = getPhotosQuery;
        _checkUserHasPhotosQuery = checkUserHasPhotosQuery;
        _memoryCachService = memoryCachService;
        _checkUserIsInDbQuery = checkUserIsInDbQuery;
    }

    public override string Name => "/view_photos";

    public override async Task Execute(Update update, ITelegramBotClient client)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            long chatId = update.Message.Chat.Id;

            if (await _checkUserHasPhotosQuery.CheckUserHasPhotosAsync(chatId))
            {
                if (await _checkUserIsInDbQuery.CheckUserIsInDbAsync(chatId) == false)
                {
                    await MessageService.SendMessage(chatId, client, _noProfileMessage, null);

                    return;
                }
                
                var photos = await _getPhotosQuery.GetUserPhotosAsync(chatId);

                if (photos != null && photos.Count > 1)
                {
                    _memoryCachService.SetMemoryCach(chatId, photos);

                    _viewPhotoMessage = new(photos[^1].Id, false);

                    await _viewPhotoMessage.SendPhoto(chatId, client, photos[photos.Count - 1].PathToPhoto);

                    return;
                }

                if (photos != null && photos.Count == 1)
                {
                    _viewPhotoMessage = new(photos[^1].Id, true);

                    await _viewPhotoMessage.SendPhoto(chatId, client, photos[photos.Count - 1].PathToPhoto);

                    return;
                }

            }

            await MessageService.SendMessage(chatId, client, _allert, null);
        }
    }
}
