static class HomeOptions
{
    public static void Options()
    {
        // Console.Clear();
        //restaurant info print here (make into json)
        Manager manager = ManagerAccess.ReadFromJson()[0];
        System.Console.WriteLine();
        System.Console.WriteLine(new string("𓌉◯ 𓇋 Jake's Restaurant 𓌉◯ 𓇋"));
        string streep = "";
        string shortIntro = $"{Reviews.AverageRating()} - €{Menu.MaxPrice()} and lower - 10:00-22:00";
        foreach(char x in shortIntro)
        {
            streep += "-";
        }
        System.Console.WriteLine(streep);
        System.Console.WriteLine(shortIntro);
        System.Console.WriteLine(streep);
        if (Home.IsLoggedIn || Home.ManagerLoggedIn)
        {
            System.Console.WriteLine($"Welkom back, {Home.guestName}!");
        }
        Console.WriteLine("[M]: Menu");
        Console.WriteLine("[R]: Reservation");
        Console.WriteLine("[RV]: Review");
        Console.WriteLine("[C]: Contact");
        Console.WriteLine("[LO]: Log Out");

        if (!Home.IsLoggedIn && !Home.ManagerLoggedIn){
        Console.WriteLine("[L]: Log in");
        Console.WriteLine("[S]: Sign up");

        }

        while (true)
        {
            string UserChoice = Console.ReadLine().ToUpper();

            switch (UserChoice)
            {
                case "L":
                    // Console.Clear();
                    Home.LogIn();
                    break;
                case "S":
                    // Console.Clear();
                    Home.SignUp();
                    break;
                case "LO":
                    Home.LogOut();
                    break;
                case "M":
                    // Console.Clear();
                    Menu.Options();
                    return;
                case "R":
                    if (Home.ManagerLoggedIn)
                    {
                    // Console.Clear();
                    manager.ReservationOptions();
                    }
                    else{
                    // Console.Clear();
                    Reservation.Options();
                    }
                    return;
                case "RV":
                if (Home.ManagerLoggedIn)
                    {
                    // Console.Clear();
                    manager.ReviewOptions();
                    }
                    else
                    {
                        // Console.Clear();
                        Reviews.Options();
                    }
                    return;
                case "C":
                    // Console.Clear();
                    Contact.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
