using Newtonsoft.Json;
public static class Menu 
{
    private static string _name = "Menu";
    public static List<MenuItem> Items { get; set; }

    public static string Name => _name;


    public static void AddItem()
    {
        //catch bad input, make item name atleast 2 char
        Console.WriteLine("What's the name of the item?");
        string itemName = Console.ReadLine();
        while (itemName.Length < 2){
        Console.WriteLine("Name must be at least 2 characters long. Please try again:");
        itemName = Console.ReadLine();
    }
        //catch bad input, make sure price is a number
        double itemPrice = 0.0;
        bool isValidPrice = false;
        do
        {
            try
            {
                Console.WriteLine("What's the price of the item? (Use a comma!)");
                string itemPrice2 = Console.ReadLine();
                if (itemPrice2.Contains(".")){
                    Console.WriteLine("Invalid input. Use a comma.");
                }
                else{
                    itemPrice = Convert.ToDouble(itemPrice2);

                    if(double.IsNegative(itemPrice)){
                        Console.WriteLine("Invalid input. Please enter a positive number for the price.");
                    }else{
                        isValidPrice = true;

                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number for the price.");
            }
        } while (!isValidPrice);


        //catch bad input, make sure item category is atleast 2 char
        Console.WriteLine("What's the category of the item?");
        string itemCategory = Console.ReadLine();
        while (itemCategory.Length < 2){
        Console.WriteLine("Category must be at least 2 characters long. Please try again:");
        itemCategory = Console.ReadLine();
        }
        MenuItem m1 = new MenuItem(itemName, itemPrice, itemCategory);
        
        //add to json
        string filePath = @"..\..\..\DataSources\Menu.json";
        StreamWriter writer = new StreamWriter(filePath, true);
        string MenuItem2Json = JsonConvert.SerializeObject(m1);
        writer.WriteLine(MenuItem2Json);
        writer.Close();

        Console.WriteLine("Item succesfully added");
        Home.Options();
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
        //if user manager give option to add/remove/change menu

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "V":
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
