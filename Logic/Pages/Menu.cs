public static class Menu 
{
    private static string _name = "Menu";
    public static List<MenuItem> Items { get; set; }

    public static string Name => _name;

    static Menu()
    {
        Items = new List<MenuItem>
        {
            new MenuItem("Salmon", 15.99, "Fish"),
            new MenuItem("Steak", 12.99, "Meat"),
            new MenuItem("Caesar Salad", 8.99, "Vegetarian"),
            new MenuItem("Soda", 2.49, "Drink"),
            new MenuItem("Iced Tea", 1.99, "Drink") 
            //CHANGE CATEGORY AND MAKE INTO JSON
        };
    }

    public static void AddItem()
    {
    }

    public static void RemoveItem()
    {
    }

    public static void ChangeItem() 
    {
    }

    public static void DisplayMenu()
    {
    }

    public static void Options()
    {
        Console.WriteLine("[H]: Home");
        Console.WriteLine("[V] View menu");
        Console.WriteLine("[VC] View a specific category");

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "V":
                    // View menu
                    return;
                case "VC":
                    // View specific category
                    return;
                case "H":
                    Home.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
