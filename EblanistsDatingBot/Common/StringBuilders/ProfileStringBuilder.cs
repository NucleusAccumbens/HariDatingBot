namespace EblanistsDatingBot.Common.StringBuilders;

public class ProfileStringBuilder
{
    private readonly DatingUserDto _userDto;

    public ProfileStringBuilder(DatingUserDto userDto)
    {
        _userDto = userDto;
    }

    public string GetProfileInfo()
    {
        string profileInfo = string.Empty;

        if (_userDto.IsVegan == true) profileInfo += "i'm a vegan\n";

        if (_userDto.IsVegan == false) profileInfo += "i'm not a vegan\n";

        if (_userDto.IsBeliever == true) profileInfo += "i believe in something\n";

        if (_userDto.IsBeliever == false) profileInfo += "i don't believe in anything\n";

        if (_userDto.IsChildfree == true) profileInfo += "i'm childfree\n";

        if (_userDto.IsChildfree == false) profileInfo += "i'm not childfree\n";

        if (_userDto.IsCosmopolitan == true) profileInfo += "i'm cosmopolitan\n";

        if (_userDto.IsCosmopolitan == false) profileInfo += "i'm not cosmopolitan\n";

        if (_userDto.IsBdsmLover == true) profileInfo += "i like BDSM\n";

        if (_userDto.IsBdsmLover == false) profileInfo += "i don't like BDSM\n";

        if (_userDto.IsMakeupUser == true) profileInfo += "i wear makeup\n";

        if (_userDto.IsMakeupUser == false) profileInfo += "i don't wear makeup\n";

        if (_userDto.IsHeelsUser == true) profileInfo += "i wear heels\n";

        if (_userDto.IsHeelsUser == false) profileInfo += "i don't wear heels\n";

        if (_userDto.IsTattooed == true) profileInfo += "i have tattoos\n";

        if (_userDto.IsTattooed == false) profileInfo += "i don't have tattoos\n";

        if (_userDto.IsExistLover == true) profileInfo += "i like to exist\n";

        if (_userDto.IsExistLover == false) profileInfo += "i don't like to exist\n";

        if (_userDto.ShaveLegs == true) profileInfo += "i shave my legs\n";

        if (_userDto.ShaveLegs == false) profileInfo += "i don't shave my legs\n";

        if (_userDto.ShaveHead == true) profileInfo += "i shave my head\n";

        if (_userDto.ShaveHead == false) profileInfo += "i don't shave my head\n";

        if (_userDto.HaveSacred == true) profileInfo += "i have something sacred\n";

        if (_userDto.HaveSacred == false) profileInfo += "i have nothing sacred\n";

        if (_userDto.OtherInfo != null) profileInfo += $"\n<i>{_userDto.OtherInfo}</i>\n";

        if (_userDto.PassedTheStiTest == true) profileInfo += $"\n<b>i passed the STI knowledge test</b>";

        if (_userDto.PassedTheStiTest == false) profileInfo += $"\n🚫 <b>i didn't pass the STI knowledge test</b>";
        
        return profileInfo;
    }
}
