using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Menu 
{
    private static string _name = "Menu";
    public static List<MenuItem> Items { get; set; }

    public static string Name => _name;


    public static void AddItem()
    {
        // Catch bad input, make item name at least 2 characters
        Console.WriteLine("What's the name of the item?");
        string itemName = Console.ReadLine();
        while (itemName.Length < 2)
        {
            Console.WriteLine("Name must be at least 2 characters long. Please try again:");
            itemName = Console.ReadLine();
        }

        // Catch bad input, make sure price is a (positive) number
        double itemPrice = 0.0;
        bool isValidPrice = false;
        do
        {
            try
            {
                Console.WriteLine("What's the price of the item? (Use a comma!)");
                string itemPrice2 = Console.ReadLine();
                if (itemPrice2.Contains("."))
                {
                    Console.WriteLine("Invalid input. Use a comma.");
                }
                else
                {
                    itemPrice = Convert.ToDouble(itemPrice2);

                    if (double.IsNegative(itemPrice))
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number for the price.");
                    }
                    else
                    {
                        isValidPrice = true;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number for the price.");
            }
        } while (!isValidPrice);

        // Catch bad input, make sure item category is at least 2 characters
        Console.WriteLine("What's the category of the item?");
        string itemCategory = Console.ReadLine();
        while (itemCategory.Length < 2)
        {
            Console.WriteLine("Category must be at least 2 characters long. Please try again:");
            itemCategory = Console.ReadLine();
        }
        
        //make the input into an object
        MenuItem newItem = new MenuItem(itemName, itemPrice, itemCategory);

        // Read existing JSON data from the file
        string filePath = @"..\..\..\DataSources\Menu.json";
        string jsonData = File.ReadAllText(filePath);

        // Deserialize existing JSON data to a list of MenuItem objects
        List<MenuItem> menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(jsonData);

        // Check if menuItems is null after deserialization
        if (menuItems == null)
        {
            // If it's null, initialize it with an empty list
            menuItems = new List<MenuItem>();
        }
        // Add the new item to the list
        menuItems.Add(newItem);

        // Serialize the updated list of items back to JSON
        string updatedJsonData = JsonConvert.SerializeObject(menuItems, Formatting.Indented);

        // Write the updated JSON data back to the file
        File.WriteAllText(filePath, updatedJsonData);

        Console.WriteLine("Item successfully added");
        }

    public static void RemoveItem()
    {
        Console.WriteLine("What's the name of the item you want to remove?");
        string itemName = Console.ReadLine();
        string filePath = @"..\..\..\DataSources\Menu.json";

        // Read existing JSON data from the file
        string jsonData = File.ReadAllText(filePath);

        // Deserialize JSON data to a JArray
        JArray menuArray = JArray.Parse(jsonData);

        bool itemRemoved = false;

        // Iterate through the menu items
        for (int i = 0; i < menuArray.Count; i++)
        {
            JObject menuItem = (JObject)menuArray[i];
            if ((string)menuItem["Name"] == itemName)
            {
                // Remove the item from the menu array
                menuArray.RemoveAt(i);
                itemRemoved = true;
                break;
            }
        }

        if (itemRemoved)
        {
            // Serialize the updated menu array back to JSON
            string updatedJsonData = menuArray.ToString(Formatting.Indented);

            // Write the updated JSON data back to the file
            File.WriteAllText(filePath, updatedJsonData);

            Console.WriteLine($"Item '{itemName}' removed successfully.");
        }
        else
        {
            Console.WriteLine($"Item '{itemName}' not found!");
        }
    }


    public static void ChangeItem() //later if needed
    {
    }

    public static void DisplayMenu()
    {
        string filePath = @"..\..\..\DataSources\Menu.json";

        // Read all lines from the file
        string jsonString = File.ReadAllText(filePath);

        // Parse the JSON string to a JArray
        JArray menuArray = JArray.Parse(jsonString);

        // Sort alphabetically by category 
        menuArray = new JArray(menuArray.OrderBy(obj => (string)obj["Category"], StringComparer.OrdinalIgnoreCase));

        // Display the menu
        Console.WriteLine("Name          | Price   | Category");
        Console.WriteLine("---------------------------------");
        foreach (JObject menuItem in menuArray)
        {
            try
            {
                string name = (string)menuItem["Name"];
                double price = (double)menuItem["Price"];
                string category = (string)menuItem["Category"];

                Console.WriteLine($"{name,-14} | €{price,-7:0.00} | {category}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                continue;
            }
        }
    }



    public static void Options()
    {
        DisplayMenu();
        Console.WriteLine("[H]: Home");
        Console.WriteLine("[S]: Sort the menu");
        Console.WriteLine("[V]: View a specific category");

        string userChoice = Console.ReadLine().ToUpper();

        switch (userChoice)
        {
            case "S":
                SortMenuOptions();
                break;
            case "V":
                DisplayCategories();
                break;
            case "H":
                Home.Options();
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                Options(); // Restart the options loop
                break;
        }
    }

    public static void SortMenuOptions()
    {
        Console.WriteLine("[P]: Sort by price");
        Console.WriteLine("[N]: Sort by name");
        Console.WriteLine("[C]: Sort by category");
        Console.WriteLine("[G]: Go back");

        string userChoiceSort = Console.ReadLine().ToUpper();

        switch (userChoiceSort)
        {
            case "P":
                // Implement sorting by price
                Options(); // After sorting, return to options
                break;
            case "N":
                // Implement sorting by name
                Options(); // After sorting, return to options
                break;
            case "C":
                // Implement sorting by category
                // SortByCategory();
                Options(); // After sorting, return to options
                break;
            case "G":
                Options(); // Go back to main options menu
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                SortMenuOptions(); 
                break;
        }
    }

    public static void DisplayCategories()
    {
        //show every category made so far (no duplicates)
        //user inputs one of those categories and then gets all items from that category back
    }
}
