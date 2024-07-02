
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
            IfFound = true;
            System.Console.WriteLine("=====================");
            System.Console.WriteLine($"Name: {review.GuestName}");
            System.Console.WriteLine($"Rating: {review.Rating}");
            System.Console.WriteLine($"Comment: {review.Comments}");
            if (review.ReplyFromManager != null){
            System.Console.WriteLine($"Restaurant's response: {review.ReplyFromManager}");
            }
            }
        }
        if (IfFound == false){
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"No review found with the rating of {Rating}."); Console.ResetColor();
        }
    }

    public static void DeleteReview() // Alleen manager
    {
    while (true)
    {
        Console.WriteLine("What is the review id? (Enter 'Q' to quit)");
        string reviewIDReadLine = Console.ReadLine().Trim();

        if (reviewIDReadLine.ToLower() == "q"){
            return;
        }

        if (!int.TryParse(reviewIDReadLine, out int reviewID))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter a valid integer or 'Q' to quit.");
            Console.ResetColor();
            continue; 
        }
        Reviews.Remove(reviewID);
        break;
    }
    }

    public static void DeleteAllReviews()
    {
        Reviews.RemoveAll();
        System.Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Successfully removed"); Console.ResetColor();

    }
    // added to reply to reviews
    public static void ReplyFromManager()
    {
        Console.WriteLine("enter the reviewID: (enter q to quit)");
        string enter = Console.ReadLine();
        if (enter == ""){
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Enter a valid ID"); Console.ResetColor();
            return;
        }
        if(enter == "q")
        {
            return;
        }
        foreach (var test in enter)
        {
            if (!char.IsDigit(test))
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Only enter numbers"); Console.ResetColor();
                Console.WriteLine("");
                ManagerOptions.ReviewOptions();
            }
        }
        int reviewID = Convert.ToInt32(enter);
        string reply;
        do
        {
            Console.WriteLine("Enter your reply: (enter q to quit)");
            reply = Console.ReadLine().ToLower();
            if (string.IsNullOrEmpty(reply) || string.IsNullOrWhiteSpace(reply))
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please enter a message"); Console.ResetColor();
            }
            else if( reply == "q")
            {
                return;
            }
        } while(string.IsNullOrEmpty(reply) || string.IsNullOrWhiteSpace(reply));

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
