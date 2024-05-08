public class Reviews
{
    private static string _name = "Reviews";
    public static List<Review> _reviews = PutInList();	
    public static string Name => _name;


    static Reviews()
    {
        LoadReviews();
    }


    private static void LoadReviews()
    {
        _reviews = ReviewAccess.ReadFromJson();
    }

    private static List<Review> PutInList()
    {
        _reviews = ReviewAccess.ReadFromJson();
        return _reviews;
    }


    private static void SaveReviews()
    {
        ReviewAccess.WriteToJson(_reviews);
    }

    public static string AverageRating()
    {
        if (_reviews.Count == 0 || _reviews == null)
            return "☆☆☆☆☆ 0";
        string AverageReview = new string("");
        double AverageRating = 0;
       foreach(Review review in _reviews)
       {
        int Count = 0;
        foreach( char star in review.Rating)
        {
            Count++;
        }
        AverageRating += Count;
       }
       try{
        AverageRating = AverageRating / _reviews.Count;
       }
       catch(Exception)
       {
        AverageRating = 0;
       }
       if ( AverageRating > 0)
       {
        for (int i = 1; i <= (int)Math.Ceiling(AverageRating); i++)
        {
            AverageReview += new string("★");
        }
       }
        if (AverageRating != 5)
        {
        for (int j = (int)Math.Ceiling(AverageRating) + 1; j <= 5; j++)
        {
            AverageReview += new string("☆");
        }
        }
        return $"{AverageReview} {Math.Round(AverageRating, 1)}";
    }

    public static void Remove(int reviewID)
    {
        int ID = 0;
        for(int i = 0; i < _reviews.Count; i++)
        {
            if (_reviews[i].ID == reviewID)
            {
                _reviews.RemoveAt(i);
            }
        }
        SaveReviews();
        LoadReviews();
    }

    public static void RemoveAll()
    {
        _reviews.Clear();
        SaveReviews();
        LoadReviews();
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
                    int ID = 0;
                    if (_reviews == null || _reviews.Count == 0)
                    {
                        _reviews = new List<Review>();
                        ID = 1;
                    }
                    else
                    {
                        ID = _reviews[_reviews.Count - 1].ID + 1;
                    }

                    Review newReview = new Review(ID, guestName, stars, comments);

                    if (newReview != null)
                    {
                        _reviews.Add(newReview);
                        SaveReviews();
                        LoadReviews();
                        Console.WriteLine("\nThank you for your review!"); 
                    }
                    Reviews.Options();
                    break;

                case "S":
                    // View reviews
                    if (_reviews == null || _reviews.Count == 0)
                    {
                        Console.WriteLine("No reviews available.");
                    }
                    else
                    {
                        ReviewLogic.SeeAllReviews();
                        Reviews.Options();
                    }
                    break;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}


