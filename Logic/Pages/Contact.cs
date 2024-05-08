public static class Contact 
{
    private static string _name = "Contact";

    public static string Name
    {
        get => _name;
    }

    public static void Options()
    {
        Console.WriteLine("[H]: Home");
        Console.WriteLine("[CI]: Contact Information"); 

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "H":
                    Home.Options();
                    return;

                case "CI":
                    // Add logic for the specific contact option
                    Console.WriteLine("For further questions you could contact us with the following:\n");
                    Console.WriteLine("Restaurant Service Phone Number: 0611223344");
                    Console.WriteLine("Restaurant Service Email: JakesRestaurant@Gmail.com");

                    Console.WriteLine("Its quite busy these days in our restaurant because of the quality and fresh food we serve, so please have patience if we take a bit longer than usual.\n\n\n");
                    Home.Options();
                    return;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
