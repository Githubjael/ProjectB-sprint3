
using System.Threading;
static class AccountManagment
{
    public static void LogIn()
    {
        // Log in als manager of log in als guest bla bla bla
        while (!Home.IsLoggedIn || !Home.ManagerLoggedIn)
        {
            System.Console.WriteLine("(At any time type 'Q' to go back)");
            Console.WriteLine("Email address:");
            string email = Console.ReadLine();
            if (email.ToLower() == "q"){
            return;
            }
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            if (password.ToLower() == "q"){
            return;
            }
            var user = UsersAccess.GetUser(email);
            Manager manager = ManagerAccess.ReadFromJson()[0];
            if (user != null && user.Password == password)
            {
                Console.WriteLine("Logged in successfully!");
                Home.IsLoggedIn = true;
                Home.guestEmail = user.EmailAddress;
                Home.guestName = user.FirstName;
                Home.ManagerLoggedIn = false;
                Console.WriteLine();
                Console.WriteLine();
            }
            else if (user == null && manager.EmailAddress == email && manager.Password == password)
            {
                System.Console.WriteLine("Manager code:");
                string ManagerCode = Console.ReadLine();
                if (ManagerCode == manager.EmployeeCode)
                {
                    System.Console.WriteLine($"Welcome back, {manager.FirstName}");
                    Home.ManagerLoggedIn = true;
                    Home.IsLoggedIn = false;
                    Home.Options();
                }
            }
            else
            {
                Console.WriteLine("Invalid email or password. Please try again.");
            }
    }
    }

   public static void LogOut()
    {
        if (Home.IsLoggedIn)
        {
            System.Console.WriteLine();
            Console.WriteLine($"Logging out {Home.guestName}...");
            System.Threading.Thread.Sleep(1500);
            Home.IsLoggedIn = false;
            Home.guestName = null;
            Home.guestEmail = null;
        }
        else if (Home.ManagerLoggedIn)
        {
            Console.WriteLine("Logging out manager...");
            Home.ManagerLoggedIn = false;
        }
        else
        {
            Console.WriteLine("No user logged in.");
        }
    }  


    public static void SignUp()
    {
        string firstName;
        do{
        System.Console.WriteLine("(At any time type 'Q' to go back)");
        System.Console.WriteLine("Enter a valid first name:");
        firstName = Console.ReadLine();
        if (firstName.ToLower() == "q"){
            return;
        }
        } while (!CheckUserInfo.IsValidName(firstName));


        string lastName;
        do{
        System.Console.WriteLine("Enter a valid last name:");
        lastName = Console.ReadLine();
        if (lastName.ToLower() == "q"){
            return;
        }
        } while (!CheckUserInfo.IsValidName(lastName));


        string email;
        do{
        System.Console.WriteLine("Enter a valid email:");
        email = Console.ReadLine();
        if (email.ToLower() == "q"){
        return;
        }
        } while (!CheckReservationInfo.CheckEmail(email));

        string phoneNumber;
        do{
            Console.WriteLine("Enter a valid phone number:");
            phoneNumber = Console.ReadLine();
            if (phoneNumber.ToLower() == "q"){
            return;
        }
        } while (!CheckUserInfo.IsValidPhoneNumber(phoneNumber));


        string password;
        do{
            Console.WriteLine("Enter a valid password:");
            password = Console.ReadLine();
            if (password.ToLower() == "q"){
            return;
        }
        } while (password == "");

        Console.WriteLine("Confirm your password:");
        string confirmPassword = Console.ReadLine();
        if (confirmPassword.ToLower() == "q"){
        return;
        }

        if (password != confirmPassword)
        {
            do
            {
                Console.WriteLine("Passwords do not match. Please try again.");
                confirmPassword = Console.ReadLine();
                if (confirmPassword.ToLower() == "q"){
            return;
        }
            } while  (password != confirmPassword);
        }

        Guest guest = new Guest(firstName, lastName, email, phoneNumber, password);
        Home.ChangeGuest(guest);
        Console.WriteLine($"Account created successfully. Thank you for signing up {guest.FirstName}!");
        System.Console.WriteLine("Make sure you log in");
    }
}
