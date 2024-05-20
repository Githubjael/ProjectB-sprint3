static class ContactOptions
{

    public static void Options()
    {
        Console.WriteLine("[1]: Home");
        Console.WriteLine("[2]: Contact Information"); 

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "1":
                    Home.Options();
                    return;

                case "2":
                    SeeContactInfo();
                    Console.WriteLine("Its quite busy these days in our restaurant because of the quality and fresh food we serve, so please have patience if we take a bit longer than usual.\n\n\n");
                    Home.Options();
                    return;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
    public static void SeeContactInfo()
    {
        Console.WriteLine("╔══════════════════════ CONTACT INFORMATION ════════════════════════╗");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("                     Welcome to Jake's Restaurant!               ");
        System.Console.WriteLine("               Please find our contact information below               ");
        System.Console.WriteLine(" ");
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine($"                      ⚲ Address: {RestaurantInfo.Adress}");
        System.Console.WriteLine($"                     ☏ Phone number: {RestaurantInfo.PhoneNumber}");
        System.Console.WriteLine($"                  ✉ Email: {RestaurantInfo.Email}");
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine("╚═══════════════════════════════════════════════════════════════════╝");
    }
}
}
