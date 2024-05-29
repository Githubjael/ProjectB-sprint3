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
        Console.WriteLine("[1]: Menu");
        Console.WriteLine("[2]: Reservation");
        Console.WriteLine("[3]: Review");
        Console.WriteLine("[4]: Contact");
        if (Home.IsLoggedIn || Home.ManagerLoggedIn){
            Console.WriteLine("[5]: Log Out");
            Console.WriteLine("[6]: Change Password");
        }

        if (!Home.IsLoggedIn && !Home.ManagerLoggedIn){
        Console.WriteLine("[5]: Log in");
        Console.WriteLine("[6]: Sign up");

        }

        while (true)
        {
            string UserChoice = Console.ReadLine().ToUpper();

            switch (UserChoice)
            {
                case "5":
                    if (!Home.IsLoggedIn && !Home.ManagerLoggedIn){
                    // Console.Clear();
                        Home.LogIn();
                        Home.Options(); 

                    }
                    else if(Home.IsLoggedIn || Home.ManagerLoggedIn){
                        Home.LogOut();
                        Home.Options(); 
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                    }
                    break;
                case "6":
                    if (Home.IsLoggedIn || Home.ManagerLoggedIn)
                    {
                        AccountManagment.ChangePassword();
                    }
                    else if (!Home.IsLoggedIn && !Home.ManagerLoggedIn){
                        Home.SignUp();
                        Home.Options(); 
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                    }
                    Options();
                    return;
                case "1":
                    // Console.Clear();
                    Menu.Options();
                    return;
                case "2":
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
                case "3":
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
                case "4":
                    // Console.Clear();
                    Contact.Options();
                    return;
                default:
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                    break;
            }
        }
    }

}
