public static class Reviews
{
    private static List<Review> _reviews = new List<Review>();

    static Reviews()
    {
        LoadReviews();
    }

    private static void LoadReviews()
    {
        _reviews = ReviewAccess.ReadFromJson();
    }

    private static void SaveReviews()
    {
        ReviewAccess.WriteToJson(_reviews);
    }

    public static string AverageRating()
    {
        if (_reviews == null || _reviews.Count == 0)
            return "☆☆☆☆☆ 0";

        double averageRating = _reviews
            .Select(review => review.Rating.Count(c => c == '★'))
            .Average();

        string averageReview = new string('★', (int)Math.Round(averageRating)) +
                               new string('☆', 5 - (int)Math.Round(averageRating));

        return $"{averageReview} {Math.Round(averageRating, 1)}";
    }

    public static void Remove(int reviewID)
    {
        _reviews.RemoveAll(review => review.ID == reviewID);
        SaveReviews();
        LoadReviews();
    }

    public static void ReplyToReview(int reviewID, string reply)
    {
        var review = _reviews.FirstOrDefault(r => r.ID == reviewID);
        if (review != null)
        {
            review.ReplyFromManager = reply;
            SaveReviews();
            LoadReviews();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Review not found");
            Console.ResetColor();
        }
    }

    public static void RemoveAll()
    {
        _reviews.Clear();
        SaveReviews();
        LoadReviews();
    }

    public static void Options()
    {
        Console.WriteLine("[1]: Home");
        Console.WriteLine("[2]: Leave a review");
        Console.WriteLine("[3]: See all reviews");

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "1":
                    Home.Options();
                    return;
                case "2":
                    LeaveReview();
                    break;
                case "3":
                    if (_reviews == null || _reviews.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No reviews available.");
                        Console.ResetColor();
                    }
                    else
                    {
                        ReviewLogic.SeeAllReviews();
                        Console.WriteLine("[1]: Home");
                        Console.WriteLine("[2]: Leave a review");
                        Console.WriteLine("[3]: See all reviews");
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    private static void LeaveReview()
    {
        if (!Home.IsLoggedIn)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please log in to leave a review.");
            Console.ResetColor();
            Console.WriteLine("[1]: Home");
            Console.WriteLine("[2]: Leave a review");
            Console.WriteLine("[3]: See all reviews");
            return;
        }

        string guestName = Home.guestName;
        Console.WriteLine("(At any time type 'Q' to go back)");
        Console.WriteLine("Rate our restaurant (from 1 to 5):");
        string input = Console.ReadLine();
        int rating = 0;
        while (input.ToLower() != "q" && (!int.TryParse(input, out rating) || rating < 1 || rating > 5))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5 (without a decimal).");
            Console.ResetColor();
            input = Console.ReadLine();
        }

        if (input.ToLower() == "q")
        {
            Reviews.Options();
            return;
        }

        Console.WriteLine("Leave your comments (Leave empty if you do not want to comment):");
        string comments = Console.ReadLine();
        string stars = new string('★', rating);
        int id = _reviews.Count == 0 ? 1 : _reviews.Max(r => r.ID) + 1;

        Review newReview = new Review(id, guestName, stars, comments);

        _reviews.Add(newReview);
        SaveReviews();
        LoadReviews();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Thank you for your review!");
        Console.ResetColor();

        Reviews.Options();
    }
}
