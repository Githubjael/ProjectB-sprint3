public class Program
{
    private static List<Review> reviews = new List<Review>();
    public static void Main()
    {
        ReservationLogic.AddReservationFromJson(); // ouder reserveringen worden in een lijst gezet (reserveringen die al in json staan)
        bool exit = false;
        //  info abt restaurant, can be changed by manager
        RestaurantInfo resto1 = new RestaurantInfo("Wijnhaven 107\n3011 WN Rotterdam", "email", "06372382");
        Menu menu = new Menu();
        while (!exit)
    {
        System.Console.WriteLine($"----Restaurant Info----\nAdress: {resto1.Adress}\nEmail: {resto1.Email}\nPhone number: {resto1.Phone_number}\n-----------------------");
        Console.WriteLine("[1] Make a reservation");
        Console.WriteLine("[2] View the menu");
        Console.WriteLine("[3] Exit");
        Console.WriteLine("[4] Login as manager");
        Console.WriteLine("[5] Cancel a reservation");
        Console.WriteLine("[6] Review our restaurant");
        Console.WriteLine("[7] See reviews");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Reservation.MakeReservation();
                break;

            case "2":
                    bool ExitMenuView = false;
                    while (!ExitMenuView)
                    {
                    Console.WriteLine("[1] View a specific category");
                    Console.WriteLine("[2] View the full menu");
                    Console.WriteLine("[3] Exit");
                    Console.Write("Enter your choice: ");
                    string choiceMenuView = Console.ReadLine();

                    switch (choiceMenuView)
                    {
                    case "1":
                    //make it so bad user input doesnt crash program for all of these
                    Console.WriteLine("fish or meat or vegetarian or drinks"); //make it so that upper/lowercase also works
                    Console.Write("Enter your choice: ");
                    string UserChoiceMenuView = Console.ReadLine();
                    menu.DisplayMenu(UserChoiceMenuView);
                        
                    break;

                    case "2":
                        Console.WriteLine("---------Menu----------");
                        menu.DisplayMenu("fish");
                        menu.DisplayMenu("meat");
                        menu.DisplayMenu("vegetarian");
                        menu.DisplayMenu("drinks");
                        Console.WriteLine("");
                    break;

                    case "3":
                    ExitMenuView = true;
                    Console.WriteLine("Exited the menu view");
                    break;

                    default:
                            Console.WriteLine("Invalid choice. Please choose a number between 1 and 3");
                    break;
                    }
                    }
                break;

            case "3":
                Console.WriteLine("Exited the program");
                exit = true;  
                break;
        
            case "4":

        //  check login and password
                bool exitManager = false;
                while (!exitManager)
                {
                Console.WriteLine("[1] Change the restaurant info");
                Console.WriteLine("[2] Change the menu"); 
                Console.WriteLine("[3] See all reservations");  //do later
                Console.WriteLine("[4] Exit");
                Console.Write("Enter your choice: ");
                string choice_manager = Console.ReadLine(); 
            
                switch (choice_manager)
                {
                    case "1":
                        //make it so bad user input doesnt crash program for all of these
                        Console.WriteLine("What is the current addres?"); // min/max char or other req?
                        string restaurant_adress = Console.ReadLine();
                        Console.WriteLine("What is the current email"); //make sure its email
                        string restaurant_email = Console.ReadLine();
                        Console.WriteLine("What is the current phone number?"); //make sure its 10 numbers
                        string phone_number = Console.ReadLine();
                        resto1.Adress = restaurant_adress;
                        resto1.Email = restaurant_email;
                        resto1.Phone_number = phone_number;
                        System.Console.WriteLine("Succesfully changed the restaurant info!");

                        break;
                
                    case "2":
                //  make it another while loop and a menu
                //  options are view menu, remove from menu and add to menu
                        bool exitMenuManager = false;
                        while (!exitMenuManager)
                        {
                            Console.WriteLine("[1] View the menu");
                            Console.WriteLine("[2] Add item to the menu");
                            Console.WriteLine("[3] Remove item from the menu");
                            Console.WriteLine("[4] Change an item from the menu");
                            Console.WriteLine("[5] Exit");
                            Console.Write("Enter your choice: ");
                            string menuManagerChoice = Console.ReadLine();
                            switch (menuManagerChoice)
                            {
                                case "1":
                                        bool ExitMenuViewManager = false;
                                        while (!ExitMenuViewManager)
                                        {
                                        Console.WriteLine("[1] View a specific category");
                                        Console.WriteLine("[2] View the full menu");
                                        Console.WriteLine("[3] Exit");
                                        Console.Write("Enter your choice: ");
                                        string choiceMenuView = Console.ReadLine();

                                        switch (choiceMenuView)
                                        {
                                        case "1":
                                        //make it so bad user input doesnt crash program for all of these
                                        Console.WriteLine("fish or meat or vegetarian or drinks"); //make it so that upper/lowercase also works
                                        Console.Write("Enter your choice: ");
                                        string UserChoiceMenuView = Console.ReadLine();
                                        menu.DisplayMenu(UserChoiceMenuView);
                                            
                                        break;

                                        case "2":
                                            Console.WriteLine("---------Menu----------");
                                            menu.DisplayMenu("fish");
                                            menu.DisplayMenu("meat");
                                            menu.DisplayMenu("vegetarian");
                                            menu.DisplayMenu("drinks");
                                            Console.WriteLine("");
                                        break;

                                        case "3":
                                        ExitMenuView = true;
                                        Console.WriteLine("Exited the menu view");
                                        break;

                                        default:
                                                Console.WriteLine("Invalid choice. Please choose a number between 1 and 3");
                                        break;
                                        }
                                        }
                                    break;

                                case "2":
                                    //make it so bad user input doesnt crash program for all of these
                                    Console.WriteLine("What is the name of the item?"); // maybe max/min char?
                                    string itemName = Console.ReadLine();
                                    Console.WriteLine("What is the price of the item? (use comma) "); //make it so . also works?
                                    double price = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Is it meat? (true/false)"); 
                                    bool isMeat = Convert.ToBoolean(Console.ReadLine());
                                    Console.WriteLine("Is it fish? (true/false)");
                                    bool isFish = Convert.ToBoolean(Console.ReadLine());
                                    Console.WriteLine("Is it vegetarian? (true/false)");
                                    bool isVegetarian = Convert.ToBoolean(Console.ReadLine());
                                    Console.WriteLine("Is it a drink? (true/false)");
                                    bool isDrink = Convert.ToBoolean(Console.ReadLine());

                                    MenuItem newItem = new MenuItem(itemName, price, isFish, isMeat, isVegetarian, isDrink);
                                    menu.AddItem(newItem);
                                    Console.WriteLine("Item added to the menu successfully!");
                                    break;

                                case "3":
                                //   Remove item from the menu
                                //make it so bad user input doesnt crash program for all of these
                                        Console.WriteLine("What is the name of the item you want to remove?");
                                        string itemToRemove = Console.ReadLine();
                                        System.Console.WriteLine(menu.RemoveItem(itemToRemove));
                                    break;

                                case "4":
                                //   Change item from the menu
                                //make it so bad user input doesnt crash program for all of these
                                        Console.WriteLine("What is the name of the item you want to change");
                                        string itemToChange = Console.ReadLine();
                                        System.Console.WriteLine(menu.ChangeItem(itemToChange));
                                    break;
                                case "5":
                                    exitMenuManager = true;
                                    break;

                            default:
                                Console.WriteLine("Invalid choice. Please choose a number between 1 and 5");
                                break;
                                        }
                                    }
                                    break;

                                        case "3":
                                    //  see an overview of all reservations

                                            break;
                                        case "4":
                                            Console.WriteLine("Exited the program");
                                            exitManager = true; 
                                            break;
                                    }
                                }
                                    break;

                case "5":
                    int guestId = 0;
                    bool validInput = false;
                    
                    do
                    {
                        Console.WriteLine("Enter your Guest ID:");
                        string input = Console.ReadLine();

                        if (string.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Invalid input. You must type something. Please enter a valid Guest ID.");
                        }
                        else if (!input.All(char.IsDigit))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid numeric number as Guest ID.");
                        }
                        else
                        {
                            guestId = Convert.ToInt32(input);
                            validInput = true; 
                        }
                    } while (!validInput);

                    bool reservationFound = false;
                    foreach (ReservationDataModel reservation in ReservationLogic._reservation)  
                    {
                        if (reservation.GuestID == guestId)
                        {
                            ReservationLogic.CancelReservation(guestId); 
                            reservationFound = true;
                            break;
                        }
                    }
                    if (!reservationFound)
                    {
                        Console.WriteLine($"No reservation found with the provided Guest ID: {guestId}");
                    }
                    break;
                
                case "6":
                    string guestName = "";
                    while (guestName == "")
                    {
                    Console.WriteLine("Enter your name:");
                    guestName = Console.ReadLine();
                    } 

                    Console.WriteLine("Rate our restaurant (from 1 to 5):");
                    int rating;
                    while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                    {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    }

                    Console.WriteLine("Leave your comments (Optional):");
                    string comments = Console.ReadLine();

                    string stars = new string('★', rating); 

                    Review newReview = new Review(guestName, stars, comments);

                    if (newReview != null)
                    {
                    reviews.Add(newReview);
                    ReviewAccess.WriteToJson(reviews);
                    Console.WriteLine("\nThank you for your review!"); 
                    }
                break;

                case "7":
                    // View reviews
                    if (reviews.Count == 0)
                    {
                        Console.WriteLine("No reviews available.");
                    }
                    else
                    {
                        Console.WriteLine("Reviews:");
                        foreach (var review in reviews)
                        {
                            Console.WriteLine($"Guest: {review.GuestName}, Rating: {review.Rating}, Comments: {review.Comments}");
                        }
                    }
                    break;


            default:
                Console.WriteLine("Invalid choice. Please choose a number between 1 and 7");
                break;
        }
    }
    }
}
