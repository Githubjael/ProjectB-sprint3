public static class Home 
{
    public static string IsLoggedIn { get; private set; }
    private static string _name = "Home";
    public static string Name
    {
        get => _name;
    }

    public static void Options()
    {
        //restaurant info print here (make into json)
        Console.WriteLine("[M]: Menu");
        Console.WriteLine("[R]: Reservation");
        Console.WriteLine("[RV]: Review");
        Console.WriteLine("[C]: Contact");
        Console.WriteLine("[L]: Log in");
        Console.WriteLine("[S]: Sign up");


        while (true)
        {
            string UserChoice = Console.ReadLine().ToUpper();

            switch (UserChoice)
            {
                case "L":
                    LogIn();
                    return;
                case "S":
                    SignUp();
                    return;
                case "M":
                    Menu.Options();
                    return;
                case "R":
                    Reservation.Options();
                    return;
                case "RV":
                    Reviews.Options();
                    return;
                case "C":
                    Contact.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    public static void SignUp()
    {
        Console.WriteLine("Enter your first name:");
        string firstName = Console.ReadLine();
        
        Console.WriteLine("Enter your last name:");
        string lastName = Console.ReadLine();
        
        Console.WriteLine("Enter your email address:");
        string email = Console.ReadLine();
        
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();
        
        Console.WriteLine("Confirm your password:");
        string confirmPassword = Console.ReadLine();

        // Store the information in JSON
    }

    public static void LogIn()
    {
        Console.WriteLine("Email address:");
        string email = Console.ReadLine();
        
        Console.WriteLine("Password:");
        string password = Console.ReadLine();
        
        // Check with JSON
        // If email and password match --> IsLoggedIn = true;
        // If password doesn't match, allow the user to proceed without logging in
    }
}
