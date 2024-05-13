static class ContactOptions
{
        public static void Options()
    {
        SeeContactInfo();
        Console.WriteLine("[H]: Home");
        Console.WriteLine(""); 

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
    public static void SeeContactInfo()
    {
        Console.WriteLine(RestaurantInfo.Adress);
        Console.WriteLine(RestaurantInfo.PhoneNumber);
        Console.WriteLine(RestaurantInfo.Email);
    }
}
