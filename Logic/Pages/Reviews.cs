public static class Reviews
{
    private static string _name = "Reviews";

    public static string Name => _name;

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

                    return;
                case "S":

                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
