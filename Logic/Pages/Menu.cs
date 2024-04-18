using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            Console.WriteLine("What's the name of the item you want to remove?");
            string itemName = Console.ReadLine();
            string filePath = @"..\..\..\DataSources\Menu.json";
            
            
            // Read each line separately and remove the item if found
            string[] lines = File.ReadAllLines(filePath);
            bool itemRemoved = false;
            for (int i = 0; i < lines.Length; i++)
            {
                try
                {
                    JObject item = JObject.Parse(lines[i]);
                    if ((string)item["Name"] == itemName)
                    {
                        // Remove the item from the array
                        lines[i] = "";
                        itemRemoved = true;
                        break;
                    }
                }
                catch (JsonReaderException)
                {
                    continue;
                }
            }


            if (itemRemoved)
            {
                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"Item '{itemName}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' not found!");
            }
            Home.Options();
        }

    public static void ChangeItem() //later if needed
    {
    }

    public static void DisplayMenu()
    {
        string filePath = @"..\..\..\DataSources\Menu.json";

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Sort alphabetically category 
        Array.Sort(lines, (x, y) =>
        {
            JObject menuObjectX = JObject.Parse(x);
            JObject menuObjectY = JObject.Parse(y);

            string categoryX = (string)menuObjectX["Category"];
            string categoryY = (string)menuObjectY["Category"];

            return categoryX.CompareTo(categoryY);
        });
        
        //display the menu
        Console.WriteLine("Name          | Price   | Category");
        Console.WriteLine("---------------------------------");
        foreach (string line in lines)
        {
            JObject menuObject = JObject.Parse(line);
            string name = (string)menuObject["Name"];
            double price = (double)menuObject["Price"];
            string category = (string)menuObject["Category"];

            Console.WriteLine($"{name,-14} | €{price,-7:0.00} | {category}");
        }
    }

    public static void Options()
    {
        DisplayMenu();
        Console.WriteLine("[H]: Home");
        Console.WriteLine("[S]: Sort the menu");
        Console.WriteLine("[V]: View a specific category");
        //if user manager give option to add/remove/change menu

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "S":
                //view menu
                //give options to sort by price, name and category
                    return;
                case "V":
                    // View specific category
                    //give of all categories made ?
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
