namespace EblanistsDatingBot.Common.Services;

public class TextCommandService
{
    public static bool CheckMessageIsCommand(string message)
    {
        string[] commandCollection = new string[]
        { "/start", "/dating", "/requests", "/profile",
            "/view_photos", "/delete_profile", "/help", "/feedback"};

        for (int i = 0; i < commandCollection.Length; i++)
        {
            if (commandCollection[i].Contains(message))
            {
                return true;
            }
        }
        return false;
    }

    public static string CheckStringLessThan500(string message)
    {
        if (message.Length > 500)
        {
            return message.Substring(0, 500);
        }

        return message;
    }
}
