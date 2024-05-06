public class Review
{
    public int ID { get; set; }
    public string GuestName { get; set; }
    public string Rating { get; set; }
    public string Comments { get; set; }

    public Review(int Id, string guestName, string rating, string comments)
    {
        ID = Id;
        GuestName = guestName;
        Rating = rating;
        Comments = comments;
    }
}
