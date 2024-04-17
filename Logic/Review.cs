public class Review
{
    public string GuestName { get; set; }
    public int Rating { get; set; }
    public string Comments { get; set; }

    public Review(string guestName, int rating, string comments)
    {
        GuestName = guestName;
        Rating = rating;
        Comments = comments;
    }
}
