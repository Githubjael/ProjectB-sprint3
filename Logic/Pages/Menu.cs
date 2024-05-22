using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Menu 
{
    private static string _name = "Menu";
    public static List<MenuItem> Items { get; set; }

    public static string Name => _name;

    private static string WayToSort = "Category";

    private static string filePath = @"..\..\..\DataSources\Menu.json";

    public static string MaxPrice()
    {
        using StreamReader reader = new(filePath);
        var json = reader.ReadToEnd();
        List<MenuItem> Menu = JsonConvert.DeserializeObject<List<MenuItem>>(json);
        double MaxPrice = 0;
        foreach(MenuItem item in Menu)
        {
            if(item.Price > MaxPrice)
            {
                MaxPrice = item.Price;
            }
        }
        return $"{MaxPrice}";
    }

    public static void AddItem()
    {
        // Catch bad input, make item name at least 2 characters
        System.Console.WriteLine("(At any time type 'Q' to go back)");
        Console.WriteLine("What's the name of the item?");
        string itemName = Console.ReadLine().Trim();
        if (itemName.ToLower() == "q"){
            return;
        }
        bool ifInJsonFile = false;
        // Deserialize existing JSON data to a list of MenuItem objects
        List<MenuItem> menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(File.ReadAllText(filePath));

        // Check if menuItems is null after deserialization
        if (menuItems == null)
        {
            // If it's null, initialize it with an empty list
            menuItems = new List<MenuItem>();
        }

        // Check if the item name already exists
        if (menuItems.Any(item => item.Name.ToLower() == itemName.ToLower()))
        {
            Console.WriteLine("Item with the same name already exists. Please choose a different name.");
            AddItem(); // Restart the method to prompt for a new item
            return;
        }
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
                Console.WriteLine("What's the price of the item? (For example: 5,00)");
                string itemPrice2 = Console.ReadLine();
                if (itemPrice2.ToLower() == "q"){
                    return;
                }
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
        if (itemCategory.ToLower() == "q"){
        return;
        }
        while (itemCategory.Length < 2)
        {
            Console.WriteLine("Category must be at least 2 characters long. Please try again:");
            itemCategory = Console.ReadLine();
        }

        Console.WriteLine("What ingredients are in the items? (Please enter at least two ingredients, separated by commas)");
        string input = Console.ReadLine();


        if (input.ToLower() == "q")
        {
            return;
        }
        List<string> ingredients = input.Split(',')
                                        .Select(ingredient => ingredient.Trim())
                                        .Where(ingredient => !string.IsNullOrEmpty(ingredient))
                                        .ToList();

        while (ingredients.Count < 2)
        {
            Console.WriteLine("You must enter at least 2 ingredients. Please try again:");
            input = Console.ReadLine();

            
            if (input.ToLower() == "q")
            {
                return;
            }

            ingredients = input.Split(',')
                            .Select(ingredient => ingredient.Trim())
                            .Where(ingredient => !string.IsNullOrEmpty(ingredient))
                            .ToList();
        }
        // Console.WriteLine("You entered the following ingredients:");
        // foreach (string ingredient in ingredients)
        // {
        //     Console.WriteLine(ingredient);
        // }

        //make the input into an object
        MenuItem newItem = new MenuItem(itemName, itemPrice, itemCategory, ingredients);

        // Read existing JSON data from the file
        string jsonData = File.ReadAllText(filePath);

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
        System.Console.WriteLine("(At any time type 'Q' to go back)");
        Console.WriteLine("What's the name of the item you want to remove?");
        string itemName = Console.ReadLine();
        if (itemName.ToLower() == "q"){
        return;
        }

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

    public static void SearchItem(){ //maybe add later

    }
    public static void ChangeItem(){ //maybe add later
    }

    public static void DisplayMenu(string HowToSort)
    {
        // Read all lines from the file
        string jsonString = File.ReadAllText(filePath);

        // Parse the JSON string to a JArray
        JArray menuArray = JArray.Parse(jsonString);

        // Sort alphabetically by category
    if (HowToSort == "Category")
    {
        menuArray = new JArray(menuArray.OrderBy(obj => (string)obj["Category"], StringComparer.OrdinalIgnoreCase));
    }
    else if (HowToSort == "Price")
    {
        menuArray = new JArray(menuArray.OrderBy(obj => (double)obj["Price"]));
    }
    else if (HowToSort == "Name")
    {
        menuArray = new JArray(menuArray.OrderBy(obj => (string)obj["Name"], StringComparer.OrdinalIgnoreCase));
    }


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
        DisplayMenu(WayToSort);
        Console.WriteLine("[1]: Home");
        Console.WriteLine("[2]: Sort the menu");
        Console.WriteLine("[3]: View a specific category");
        if (Home.ManagerLoggedIn){
        Console.WriteLine("[4]: Add item");
        Console.WriteLine("[5]: Remove item");
        }

        string userChoice = Console.ReadLine().ToUpper();

        switch (userChoice)
        {
            case "2":
                SortMenuOptions();
                break;
            case "3":
                DisplayCategories();
                break;
            case "1":
                Home.Options();
                break;
            case "4":
                if(Home.ManagerLoggedIn){
                    AddItem();
                }
                else{
                    Console.WriteLine("Invalid input. Please try again."); 
                }
                Options();
                break;
            case "5":
                if(Home.ManagerLoggedIn){
                    RemoveItem();
                }
                else{
                    Console.WriteLine("Invalid input. Please try again.");
                }
                Options();
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                Options(); // Restart the options loop
                break;
        }
    }

    public static void SortMenuOptions()
    {
        Console.WriteLine("[1]: Sort by price");
        Console.WriteLine("[2]: Sort by name");
        Console.WriteLine("[3]: Sort by category");
        Console.WriteLine("[4]: Go back");

        string userChoiceSort = Console.ReadLine().ToUpper();

        switch (userChoiceSort)
        {
            case "1":
                // Implement sorting by price
                WayToSort = "Price";
                Options(); // After sorting, return to options
                break;
            case "2":
                // Implement sorting by name
                WayToSort = "Name";
                Options(); // After sorting, return to options
                break;
            case "3":
                // Implement sorting by category
                WayToSort = "Category";
                Options(); // After sorting, return to options
                break;
            case "4":
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
        string jsonString = File.ReadAllText(filePath);
        JArray menuArray = JArray.Parse(jsonString);

        Dictionary<string, List<JObject>> categories = new Dictionary<string, List<JObject>>();

        // Group menu items by category (category is key and linked to its value (itemname))
        foreach (JObject menuItem in menuArray)
        {
            string category = ((string)menuItem["Category"]).ToLower(); 
            if (!categories.ContainsKey(category))
            {
                categories.Add(category, new List<JObject>());
            }
            categories[category].Add(menuItem);
        }

        // Display categories to the user
        Console.WriteLine("Available Categories:");
        foreach (string category in categories.Keys)
        {
            Console.WriteLine("- " + category);
        }

        // Prompt user to input a category
        Console.WriteLine("Enter a category to view its items:");
        string selectedCategory = Console.ReadLine().ToLower();

        // Check if the selected category exists
        if (categories.ContainsKey(selectedCategory))
        {
            // Display items belonging to the selected category
            Console.WriteLine($"Items in category '{selectedCategory}':");
            Console.WriteLine("Name          | Price   | Category  | Ingredients");
            Console.WriteLine("-------------------------------------------------");
            foreach (JObject menuItem in categories[selectedCategory])
            {
                string name = (string)menuItem["Name"];
                double price = (double)menuItem["Price"];
                JArray ingredientsArray = (JArray)menuItem["Ingredients"];
                
                // Check if ingredientsArray is null
                string ingredients = ingredientsArray != null ? string.Join(", ", ingredientsArray) : "N/A";
                
                Console.WriteLine($"{name,-14} | €{price,-7:0.00} | {selectedCategory} | {ingredients, -14}");
            }
        }
        else
        {
            Console.WriteLine("Invalid category. Please try again.");
        }
        Console.WriteLine("[1]: View another category");
        Console.WriteLine("[2]: Go back");

        string userChoiceSort = Console.ReadLine().ToUpper();

        switch (userChoiceSort)
        {
            case "1":
                DisplayCategories();
                break;
            case "2":
                Options(); // Go back to main options menu
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                SortMenuOptions(); 
                break;
    }

}
}
