using EblanistsDatingBot.Common.StringBuilders;

namespace EblanistsDatingBot.Messages.UserMessages;

public class ProfileMessage : BaseMessage
{
    private readonly bool _isTextCommand;
    
    private readonly string _progressBarToThirtyPercent =
        "🔴🔴🔴⚫️⚫️⚫️⚫️⚫️⚫️⚫️ 30%";

    private readonly string _progressBarToFiftyPercent =
        "🔴🔴🔴🔴🔴⚫️⚫️⚫️⚫️⚫️ 50%";

    private readonly string _progressBarToEightyPercent =
        "🟢🟢🟢🟢🟢🟢🟢🟢⚫️⚫️ 80%";

    private readonly string _progressBarToOneРundredPercent =
        "🟢🟢🟢🟢🟢🟢🟢🟢🟢🟢 100%";

    private readonly ProfileStringBuilder _stringBuilder;

    private readonly DatingUserDto _userDto;

    public ProfileMessage(DatingUserDto datingUserDto, bool isTextCommand)
    {
        _userDto = datingUserDto;
        _stringBuilder = new(_userDto);
        _isTextCommand = isTextCommand;
    }

    public override string MessageText => 
        _stringBuilder.GetProfileInfo();

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        string progressBar = String.Empty;

        if (_userDto.OtherInfo == null  || _userDto.OtherInfo == String.Empty &&
            _userDto.PassedTheStiTest == false &&
            _userDto.HasPhotos == false) progressBar += _progressBarToThirtyPercent;

        if (_userDto.OtherInfo != null && _userDto.PassedTheStiTest == false && _userDto.HasPhotos == false ||
            _userDto.OtherInfo == null && _userDto.PassedTheStiTest == false && _userDto.HasPhotos == true ||
            _userDto.OtherInfo == null && _userDto.PassedTheStiTest == true && _userDto.HasPhotos == false) 
            progressBar += _progressBarToFiftyPercent;

        if (_userDto.OtherInfo != null && _userDto.HasPhotos == true ||
            _userDto.OtherInfo != null && _userDto.PassedTheStiTest == true)
            progressBar += _progressBarToEightyPercent;

        if (_userDto.OtherInfo != null &&
            _userDto.PassedTheStiTest == true &&
            _userDto.HasPhotos == true) progressBar += _progressBarToOneРundredPercent;

        else progressBar += String.Empty;

        if (_isTextCommand == true)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: $"{progressBar}", callbackData: "}")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 view photos  ", callbackData: $"uViewPhotos"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 pass the test", callbackData: $"qStiTest")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 edit profile  ", callbackData: $"pEditProfile"),
                },
            });
        }

        else return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: $"{progressBar}", callbackData: "}")
            },                     
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 view photos  ", callbackData: $"uViewPhotos"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 pass the test", callbackData: $"qStiTest")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 edit profile  ", callbackData: $"pEditProfile"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"_")
            },
        });
    }

}
