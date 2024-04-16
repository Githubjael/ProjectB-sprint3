class Review : Page
{
    public override string Name => "Review";

    public string GuestName{get;}
    public int Rating{get;}
    public string Comments{get;}
    public Review(string guestName, int rating, string comments)
    {
        GuestName = guestName;
        Rating = rating;
        Comments = comments;
    }
    public override void Contents()
    {
    }
}
