using System.Security.Cryptography.X509Certificates;

static class Messages
{

    public static void Greeting()
    {
        if (Home.IsLoggedIn)
            System.Console.WriteLine($"Welcome back, {Home.guestName}!");
        else if(Home.ManagerLoggedIn)
            System.Console.WriteLine($"Welcome, Jane");
    }
    public static void ReservationGreetings()
    {
        if(Home.IsLoggedIn)
        {
            System.Console.WriteLine("(At any time type 'Q' to go back)");
            System.Console.WriteLine($"Hi {Home.guestName}! please provide us with the following details:\n");
            System.Threading.Thread.Sleep(1500);
        }
        else{
            System.Console.WriteLine("You can Enter 'Q' to go back to the Home page at any time\n");
            System.Threading.Thread.Sleep(1000);
            System.Console.WriteLine("We are honored by your interest in booking at our restaurant!\n");
            System.Threading.Thread.Sleep(1000);
            System.Console.WriteLine("The amount of guests determines the type of your reservation.\n");
            System.Threading.Thread.Sleep(1000);
            System.Console.WriteLine("A reservation of 1, reserves a seat at the bar.");
            System.Console.WriteLine("A reservation of 2, reserves a 2 person table.");
            System.Console.WriteLine("A reservation of 3-4, reserves a 4 person table.");
            System.Console.WriteLine("A reservation of 5-6, reserves a 6 person table.\n");
            System.Threading.Thread.Sleep(2000);
            System.Console.WriteLine("Please answer the following questions:\n");
            System.Threading.Thread.Sleep(500);
        }  
    }

    public static void Thanking4Reservation(int GuestID)
    {
        if(Home.IsLoggedIn)
        {
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Thank you for choosing to book with us, We look forward to serving you!"); Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Thank you for choosing to book with us! Your guest ID is {GuestID}.\nIf you need to cancel your reservation, please provide this guest ID for easy processing. We look forward to serving you!"); Console.ResetColor();
        }
        
    }

    public static bool AskForPreOrder()
    {
        while (true)
        {
            Console.WriteLine("Do you want to place an order? Y/N");
            string input = Console.ReadLine().ToLower();
            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
            }
        }
    }

}
