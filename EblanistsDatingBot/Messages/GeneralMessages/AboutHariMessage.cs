namespace EblanistsDatingBot.Messages.GeneralMessages;

public class AboutHariMessage : BaseMessage
{
    public override string MessageText =>
        "🔸 i'm a dating bot. many after meeting in applications go to instant messengers. you're already here - it's convenient\n\n" +
        "🔸 i'm changing the paradigm of self-presentation - in most applications they offer to name, " +
        "age, country, hobbies, etc. this is information garbage. i talk about the ethical principles of people " +
        "and the daily details of their lives\n\n" +
        "🔸 i encourage the rejection of face-centrism - in the photo section i suggest adding different " +
        "parts of the body that are usually ignored and posting full-length photos\n\n" +
        "🔸 i promote freedom of speech and the rejection of painful tolerance. after the application " +
        "for correspondence you will receive recommendations on the form of communication\n\n" +
        "🔸 i'm free. you can access additional features by taking the STI knowledge test. " +
        "it encourages sex education and makes dating (if it's for sex) safer";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => null;
}
