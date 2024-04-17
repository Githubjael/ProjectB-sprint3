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
        Console.WriteLine("???"); 

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "H":
                    Home.Options();
                    return;

                case "???":
                    // Add logic for the specific contact option
                    return;

                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
