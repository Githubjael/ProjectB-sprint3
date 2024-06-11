    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class Menu
    {

        private static string WayToSort = "Category";

        private static string filePath = @"..\..\..\DataSources\Menu.Json";

        public static List<MenuItem> MenuItems = JsonStuff.ReadFromJson<MenuItem>(filePath);

        public static string MaxPrice()
        {
            List<MenuItem> Menu = JsonConvert.DeserializeObject<List<MenuItem>>(File.ReadAllText(filePath));
            double MaxPrice = 0;
            foreach (MenuItem item in Menu)
            {
                if (item.Price > MaxPrice)
                {
                    MaxPrice = item.Price;
                }
            }
            return $"{MaxPrice}";
        }

        private static void AddItem()
        {
            
            //turn the Json into a list of menuitems
            List<MenuItem> menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(File.ReadAllText(filePath));

            // Check if menuItems null
            if (menuItems == null)
            {
                // make an empty list to give back an empty menu
                menuItems = new List<MenuItem>();
            }

            // Catch bad input, make item name at least 2 characters
            System.Console.WriteLine("(At any time type 'Q' to go back)");
            Console.WriteLine("What's the name of the item?");
            string itemName = Console.ReadLine().Trim();
            if (itemName.ToLower() == "q")
            {
                return;
            }

            // Check if the item name already exists
            if (menuItems.Any(item => item.Name.ToLower() == itemName.ToLower()))
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Item with the same name already exists. Please choose a different name."); Console.ResetColor();
                AddItem(); // Restart the method to prompt for a new item
                return;
            }
            while (itemName.Length < 2 || itemName.Length > 18)
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Name must be at least 2 and under 18 characters long. Please try again:"); Console.ResetColor();
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
                    if (itemPrice2.ToLower() == "q")
                    {
                        return;
                    }
                    else if (itemPrice2.Contains("."))
                    {
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid input. Use a comma."); Console.ResetColor();
                    }
                    else
                    {
                        itemPrice = Convert.ToDouble(itemPrice2);

                        if (double.IsNegative(itemPrice))
                        {
                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid input. Please enter a positive number for the price."); Console.ResetColor();
                        }
                        else
                        {
                            isValidPrice = true;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid input. Please enter a number for the price."); Console.ResetColor();
                }
            } while (!isValidPrice);

            // Catch bad input, make sure item category is at least 2 characters
            Console.WriteLine("What's the category of the item?");
            string itemCategory = Console.ReadLine();
            if (itemCategory.ToLower() == "q")
            {
                return;
            }
            while (itemCategory.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Category must be at least 2 characters long. Please try again:"); Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"You must enter at least 2 ingredients. Please try again:"); Console.ResetColor();
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

            // Check if vegan
            Console.WriteLine("Is it vegan? (Y/N)");
            string IsVegan = Console.ReadLine();
            if (IsVegan.ToLower() == "q")
            {
                return;
            }
            while (IsVegan.ToLower() != "y" && IsVegan.ToLower() != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Enter Y (for yes) or N (for no). Please try again:"); Console.ResetColor();
                IsVegan = Console.ReadLine();
            }
            bool IsVeganBool = (IsVegan.ToLower() == "y") ? true : false;

            // Check if spicy
            Console.WriteLine("Is it spicy? (Y/N)");
            string IsSpicy = Console.ReadLine();
            if (IsSpicy.ToLower() == "q")
            {
                return;
            }
            while (IsSpicy.ToLower() != "y" && IsSpicy.ToLower() != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Enter Y (for yes) or N (for no). Please try again:"); Console.ResetColor();
                IsSpicy = Console.ReadLine();
            }
            bool IsSpicyBool = (IsSpicy.ToLower() == "y") ? true : false;

            string itemSymbol = "";
            if (IsVeganBool)
            {
                // Add a vegan symbol to item name
                itemSymbol += "♣";
            }

            if (IsSpicyBool)
            {
                // Add a spicy symbol to item name
                itemSymbol += "🌶";
            }

            // Generate a new unique ID
            string newId = menuItems.Count > 0 ? (int.Parse(menuItems.Max(item => item.Id)) + 1).ToString() : "1";

            // Create the new MenuItem object
            MenuItem newItem = new MenuItem(newId, itemName, itemPrice, itemCategory, ingredients, itemSymbol);

            // Add the new item to the list
            menuItems.Add(newItem);

            // write the new menu back to json
            string updatedJsonData = JsonConvert.SerializeObject(menuItems, Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonData);

            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Item '{itemName}' added successfully"); Console.ResetColor();
            System.Threading.Thread.Sleep(1500);
        }

        private static void RemoveItem()
        {
            System.Console.WriteLine("(At any time type 'Q' to go back)");
            Console.WriteLine("What's the name or ID of the item you want to remove?");
            string itemNameOrId = Console.ReadLine();
            if (itemNameOrId.ToLower() == "q")
            {
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
                if ((string)menuItem["Name"] == itemNameOrId || (string)menuItem["Id"] == itemNameOrId)
                {
                    // Remove the item from the menu array
                    menuArray.RemoveAt(i);
                    itemRemoved = true;
                    break;
                }
            }

            if (itemRemoved)
            {
                // Write the updated menu back to json
                string updatedJsonData = menuArray.ToString(Formatting.Indented);
                File.WriteAllText(filePath, updatedJsonData);

                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Item '{itemNameOrId}' removed successfully"); Console.ResetColor();
                System.Threading.Thread.Sleep(1500);

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Item '{itemNameOrId}' not found!"); Console.ResetColor();
                System.Threading.Thread.Sleep(1500);
            }
        }

        // public static void SearchItem(){ maybe add later

        // }
        // public static void ChangeItem(){ maybe add later
        // }

        public static void DisplayMenu(string HowToSort)
        {
            string jsonString = File.ReadAllText(filePath);
            JArray menuArray = JArray.Parse(jsonString);

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

            DisplayMenuItems(menuArray);
        }

        public static void Options()
        {
            DisplayMenu(WayToSort);
            Console.WriteLine("[1]: Home");
            Console.WriteLine("[2]: Sort the menu");
            Console.WriteLine("[3]: View a specific category");
            if (Home.ManagerLoggedIn)
            {
                Console.WriteLine("[4]: Add item");
                Console.WriteLine("[5]: Remove item");
            }
            while (true)
            {
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
                        if (Home.ManagerLoggedIn)
                        {
                            AddItem();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                            System.Threading.Thread.Sleep(1500);
                            Console.Clear();
                        }
                        Options();
                        break;
                    case "5":
                        if (Home.ManagerLoggedIn)
                        {
                            RemoveItem();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                            System.Threading.Thread.Sleep(1500);
                            Console.Clear();
                        }
                        Options();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                        System.Threading.Thread.Sleep(1500);
                        break;
                }
            }
        }

        private static void SortMenuOptions()
        {
            Console.WriteLine("[1]: Sort by price");
            Console.WriteLine("[2]: Sort by name");
            Console.WriteLine("[3]: Sort by category");
            Console.WriteLine("[4]: Go back");
            while (true)
            {
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
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Invalid input. Please try again."); Console.ResetColor();
                        break;
                }
            }
        }

        private static void DisplayCategories(){
            string jsonString = File.ReadAllText(filePath);
            JArray menuArray = JArray.Parse(jsonString);
            //make a dictionary that has category aa key and all menuitems as its value
            Dictionary<string, JArray> categories = new Dictionary<string, JArray>();

            foreach (JObject menuItem in menuArray)
            {
                string category = ((string)menuItem["Category"]).ToLower();
                if (!categories.ContainsKey(category))
                {
                    categories.Add(category, new JArray());
                }
                categories[category].Add(menuItem);
            }

            Console.WriteLine("Available Categories:");
            foreach (string category in categories.Keys)
            {
                //make each category first letter a capital
                string CapitalizedCategory = char.ToUpper(category[0]) + category.Substring(1);
                Console.WriteLine("- " + CapitalizedCategory);
            }

            Console.WriteLine("Enter a category to view its items:");
            string selectedCategory = Console.ReadLine().ToLower();

            if (categories.ContainsKey(selectedCategory))
            {
                Console.WriteLine($"Items in category '{selectedCategory}':");
                DisplayMenuItems(categories[selectedCategory]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Category '{selectedCategory}' was not found. Please try again.");
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.ResetColor();
                    SortMenuOptions();
                    break;
            }
        }


        private static void DisplayMenuItems(JArray menuItems){
        Console.WriteLine($"{"ID", -5}| {"Name", -19}| {"Price", -7}| {"Category", -10}| {"Ingredients"}");
        Console.WriteLine("-------------------------------------------------------------------------------------");

        foreach (JObject menuItem in menuItems)
        {
            try
            {
                string id = (string)menuItem["Id"];
                string name = (string)menuItem["Name"];
                double price = (double)menuItem["Price"];
                string category = (string)menuItem["Category"];
                string symbol = (string)menuItem["Symbol"];
                List<string> ingredients = menuItem["Ingredients"].ToObject<List<string>>();
                List<string> formattedIngredients = new List<string>();
                string currentLine = string.Empty;
                
                //format the ingredients, so it gets 50 char per line
                foreach (string ingredient in ingredients)
                {
                    if ((currentLine + ", " + ingredient).Length > 50)
                    {
                        formattedIngredients.Add(currentLine);
                        currentLine = ingredient;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentLine))
                        {
                            currentLine += ", ";
                        }
                        currentLine += ingredient;
                    }
                }
                
                //add the remaining ingredients 
                formattedIngredients.Add(currentLine);
                
                if(symbol == "🌶"){
                    Console.WriteLine($"{id,-2} {symbol,-4}| {name,-19} | €{price,-7:0.00} | {category,-10} | {formattedIngredients[0],-75}");

                }
                else{
                Console.WriteLine($"{id,-2} {symbol,-3}| {name,-19} | €{price,-7:0.00} | {category,-10} | {formattedIngredients[0],-75}");
                }
                for (int i = 1; i < formattedIngredients.Count; i++)
                {
                    Console.WriteLine($"{' ',-2} {' ',-3}| {' ',-19} |  {' ',-7:0.00} | {' ',-10} | {formattedIngredients[i],-75}");
                }


            }
            catch (Exception ex)
            {
                continue;
            }
        }
        Console.WriteLine("♣ = vegan. 🌶 = spicy.");
    }

    }

