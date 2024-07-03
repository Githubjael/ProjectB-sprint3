static class HomeOptions
{

    //please guys merge eerst met dit en dan pas je eigen erop zeten!!!!!
    public static void Options()
    {
        Console.Clear();
        //restaurant info print here (make into json)
        Manager manager = ManagerAccess.ReadFromJson()[0];
        System.Console.WriteLine();
        System.Console.WriteLine(new string("𓌉◯ 𓇋 Jake's Restaurant 𓌉◯ 𓇋"));
        string streep = "";
        var TimeSlots = Home.ShowTimeSlots();
        string shortIntro = $"{Reviews.AverageRating()} - €{Menu.MaxPrice()} and lower - {TimeSlots[0]}-{TimeSlots[TimeSlots.Count - 1]}";
        foreach(char x in shortIntro)
        {
            streep += "-";
        }
        System.Console.WriteLine(streep);
        System.Console.WriteLine(shortIntro);
        System.Console.WriteLine(streep);
        if (Home.IsLoggedIn)
        {
            System.Console.WriteLine($"Welkome back, {Home.guestName}!");
        }
        else if(Home.ManagerLoggedIn)
        {
            System.Console.WriteLine($"Welcome back, {manager.FirstName}");
        }
        Console.WriteLine("[1]: Menu");
        Console.WriteLine("[2]: Reservation");
        Console.WriteLine("[3]: Review");
        Console.WriteLine("[4]: Info"); //in de code heet het contact
        if (Home.IsLoggedIn){
            Console.WriteLine("[5]: Log Out");
            Console.WriteLine("[6]: Change Password");
        }
        if (Home.ManagerLoggedIn){
            Console.WriteLine("[5]: Log Out");
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
                    Console.Clear();
                        Home.LogIn();
                        Home.Options(); 

                    }
                    else if(Home.IsLoggedIn || Home.ManagerLoggedIn){
                        Console.Clear();
                        Home.LogOut();
                        Home.Options(); 
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                    }
                    break;
                case "6":
                Console.Clear();
                    if (Home.IsLoggedIn)
                    {
                        AccountManagment.ChangePassword();
                    }
                    else if (!Home.IsLoggedIn && !Home.ManagerLoggedIn){
                        Console.Clear();
                        Home.SignUp();
                        Home.Options(); 
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                    }
                    System.Threading.Thread.Sleep(1500);
                    Options();
                    return;

                case "1":
                    Console.Clear();
                    Menu.Options();
                    return;
                case "2":
                    if (Home.ManagerLoggedIn)
                    {
                    Console.Clear();
                    ManagerOptions.ReservationOptions();
                    }
                    else{
                    Console.Clear();
                    Reservation.Options();
                    }
                    return;
                case "3":
                if (Home.ManagerLoggedIn)
                    {
                    Console.Clear();
                    ManagerOptions.ReviewOptions();
                    }
                    else
                    {
                        Console.Clear();
                        Reviews.Options();
                    }
                    return;
                case "4":
                    Console.Clear();
                    if (!Home.ManagerLoggedIn){
                    Contact.Options();
                    }
                    else{
                        ManagerOptions.ChangeRestaurantInfo();
                    }
                    return;
                default:
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                    break;
            }
        }
    }

}
