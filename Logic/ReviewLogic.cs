
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

class ReviewLogic : IComparable<Review>
{

    public static void SeeAllReviews()
    {
        List<Review> Reviews = ReviewAccess.ReadFromJson();
        foreach(var review in Reviews)
        {
            System.Console.WriteLine("=====================");
            System.Console.WriteLine($"ID: {review.ID}");
            System.Console.WriteLine($"Name: {review.GuestName}");
            System.Console.WriteLine($"Rating: {review.Rating}");
            System.Console.WriteLine($"Comment: {review.Comments}");
            if (review.ReplyFromManager != null){
            System.Console.WriteLine($"Restaurant's response: {review.ReplyFromManager}");
            }
        }
    }

    public static void SeeReviewsBasedOnRating(int Rating) // Alleen manager
    {
        bool IfFound = false;
        string star = new string ('★', Rating);
        List<Review> Reviews = ReviewAccess.ReadFromJson();
        foreach(var review in Reviews)
        {
            if (star == review.Rating){
            System.Console.WriteLine("=====================");
            System.Console.WriteLine($"Name: {review.GuestName}");
            System.Console.WriteLine($"Rating: {review.Rating}");
            System.Console.WriteLine($"Comment: {review.Comments}");
            if (review.ReplyFromManager != null){
            System.Console.WriteLine($"Restaurant's response: {review.ReplyFromManager}");
            IfFound = true;
            }
            }
        }
        if (IfFound == false){
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"No reservation found with the rating of {Rating}."); Console.ResetColor();
        }
    }

    public static void DeleteReview() // Alleen manager
    {
        System.Console.WriteLine("What is the review id?");
        int reviewID = Convert.ToInt32(System.Console.ReadLine());
        Reviews.Remove(reviewID);
    }

    public static void DeleteAllReviews()
    {
        Reviews.RemoveAll();
        System.Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"All reviews succesfully removed."); Console.ResetColor();
    }
    // added to reply to reviews
    public static void ReplyFromManager()
    {
        Console.WriteLine("enter the reviewID: (enter q to quit)");
        string enter = Console.ReadLine();
        if(enter == "q")
        {
            return;
        }
        foreach (var test in enter)
        {
            if (!char.IsDigit(test))
            {
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Invalid input"); Console.ResetColor();
                Console.WriteLine("");
                ManagerOptions.ReviewOptions();
            }
        }
        int reviewID = Convert.ToInt32(enter);
        Console.WriteLine("Enter your reply: ");
        string reply = Console.ReadLine();
        Reviews.ReplyToReview(reviewID, reply);
    }

    public static void SortReviews()
    {

    }
    public int CompareTo(Review? other)
    {
        throw new NotImplementedException();
    }
}
