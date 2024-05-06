
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

class ReviewLogic : IComparable<Review>
{
    public static string reviewstring = @"C:\Users\User\Desktop\Project B\DataSources\Reviews.Json";
    public static void SeeAllReviews()
    {
        string reviewsstring1 = File.ReadAllText(reviewstring);
        JArray reviews = JArray.Parse(reviewsstring1);
        System.Console.WriteLine(" Id  | Name       | Rating        | Comment   ");
        System.Console.WriteLine("--------------------------------------------------");
        foreach (JObject review in reviews)
        {
            // try
            // {
                int Id = (int)review["ID"];
                string name = (string)review["GuestName"];
                string rating = (string)review["Rating"];
                string comment = (string)review["Comments"];
                Console.WriteLine($"{Id, -3} | {name,-10} | {rating, -14} | {comment}");
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Error parsing JSON: {ex.Message}");
            //     continue;
            // }
        } 
    }

    public static void SeeReviewsBasedOnRating(int Rating) // Alleen manager
    {
        string reviewsstring1 = File.ReadAllText(reviewstring);
        JArray reviews = JArray.Parse(reviewsstring1);
        string star = new string ('★', Rating);
        System.Console.WriteLine(" Id  | Name       | Rating        | Comment   ");
        System.Console.WriteLine("--------------------------------------------------");
        foreach (JObject review in reviews)
        {
            // try
            // {
                int Id = (int)review["ID"];
                string name = (string)review["GuestName"];
                string rating = (string)review["Rating"];
                string comment = (string)review["Comments"];
                if (star == rating){
                Console.WriteLine($"{Id, -3} | {name,-10} | {rating, -14} | {comment}");
                }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Error parsing JSON: {ex.Message}");
            //     continue;
            // }
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
        System.Console.WriteLine("All reviews succesfully removed.");
    }

    public static void SortReviews()
    {

    }
    public int CompareTo(Review? other)
    {
        throw new NotImplementedException();
    }
}
