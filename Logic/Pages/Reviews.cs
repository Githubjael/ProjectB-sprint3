class Reviews : Page
{

    private static List<Review> reviews = new List<Review>();
    public override string Name => "Reviews";

    static Reviews()
    {
        LoadReviews();
    }


    private static void LoadReviews()
    {
        reviews = ReviewAccess.ReadFromJson();
    }


    private static void SaveReviews()
    {
        ReviewAccess.WriteToJson(reviews);
    }

    public static void Options()
    {
        Console.WriteLine("[H]: Home");
        Console.WriteLine("[L]: Leave a review");
        Console.WriteLine("[S]: See all reviews");

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "H":
                    Home.Options();
                    return;
                case "L":
                    string guestName = "";
                    while (guestName == "")
                    {
                        Console.WriteLine("Enter your name:");
                        guestName = Console.ReadLine();
                    } 

                    Console.WriteLine("Rate our restaurant (from 1 to 5):");
                    int rating;
                    while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    }

                    Console.WriteLine("Leave your comments (Optional):");
                    string comments = Console.ReadLine();

                    string stars = new string('★', rating); 

                    Review newReview = new Review(guestName, stars, comments);

                    if (newReview != null)
                    {
                        reviews.Add(newReview);
                        SaveReviews(); 
                        Console.WriteLine("\nThank you for your review!"); 
                    }
                    break;

                case "S":
                    // View reviews
                    if (reviews.Count == 0)
                    {
                        Console.WriteLine("No reviews available.");
                    }
                    else
                    {
                        Console.WriteLine("Reviews:\n");
                        foreach (var review in reviews)
                        {
                            Console.WriteLine($"Guest: {review.GuestName}\nRating: {review.Rating}\nComments: {review.Comments}\n\n");
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    public override void Contents()
    {
        throw new NotImplementedException();
    }
}
