public class Review
{
    public string GuestName { get; set; }
    public string Rating { get; set; }
    public string Comments { get; set; }

    public Review(string guestName, string rating, string comments)
    {
        GuestName = guestName;
        Rating = rating;
        Comments = comments;
    }
}
