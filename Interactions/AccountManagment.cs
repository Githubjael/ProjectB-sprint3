using System.Threading;
static class AccountManagment
{
    public static void ChangePassword()
    {
        Console.WriteLine("Email address:");
        string email = Console.ReadLine();

        var user = UsersAccess.GetUser(email);

        if (user == null)
        {
            Console.WriteLine("User not found. Please try again.");
            return;
        }

        Console.WriteLine("Current password:");
        string currentPassword = Console.ReadLine();

        if (user.Password != currentPassword)
        {
            Console.WriteLine("Incorrect current password. Please try again.");
            return;
        }

        string newPassword;
        do
        {
            Console.WriteLine("Enter new password:");
            newPassword = Console.ReadLine();
        } while (string.IsNullOrEmpty(newPassword));

        Console.WriteLine("Confirm new password:");
        string confirmNewPassword = Console.ReadLine();

        if (newPassword != confirmNewPassword)
        {
            Console.WriteLine("Passwords do not match. Please try again.");
            return;
        }

        user.Password = newPassword;
        UsersAccess.UpdateUser(user);

        var updatedUser = UsersAccess.GetUser(email);
        if (updatedUser != null && updatedUser.Password == newPassword)
        {
            Console.WriteLine("Password changed successfully.");
        }
        else
        {
            Console.WriteLine("Password change failed. Please try again.");
        }
    }

    public static void LogIn()
    {
        // Log in as manager or guest
        while (!Home.IsLoggedIn && !Home.ManagerLoggedIn)
        {
            Console.WriteLine("Email address:");
            string email = Console.ReadLine();

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

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
                Home.Options(); 
            }
            else if (user == null && manager.EmailAddress == email && manager.Password == password)
            {
                Console.WriteLine("Manager code:");
                string ManagerCode = Console.ReadLine();
                if (ManagerCode == manager.EmployeeCode)
                {
                    Console.WriteLine($"Welcome back, {manager.FirstName}");
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
            Console.WriteLine($"Logging out {Home.guestName}...");
            Thread.Sleep(1500);
            Home.IsLoggedIn = false;
            Home.guestName = null;
            Home.guestEmail = null;
            Console.WriteLine("Logged out successfully.");
        }
        else if (Home.ManagerLoggedIn)
        {
            Console.WriteLine("Logging out manager...");
            Home.ManagerLoggedIn = false;
            Console.WriteLine("Logged out successfully.");
        }
        else
        {
            Console.WriteLine("No user logged in.");
        }

        Home.Options();

    }

    public static void SignUp()
    {
        string firstName;
        do{
        System.Console.WriteLine("Enter a valid first name:");
        firstName = Console.ReadLine();
        } while (!CheckUserInfo.IsValidName(firstName));


        string lastName;
        do{
        System.Console.WriteLine("Enter a valid last name:");
        lastName = Console.ReadLine();
        } while (!CheckUserInfo.IsValidName(lastName));


        string email;
        do{
        System.Console.WriteLine("Enter a valid email:");
        email = Console.ReadLine();
        } while (!CheckUserInfo.IsValidEmail(email));

        string phoneNumber;
        do{
            Console.WriteLine("Enter a valid phone number:");
            phoneNumber = Console.ReadLine();
        } while (!CheckUserInfo.IsValidPhoneNumber(phoneNumber));


        string password;
        do{
            Console.WriteLine("Enter a valid password:");
            password = Console.ReadLine();
        } while (password == "");

        Console.WriteLine("Confirm your password:");
        string confirmPassword = Console.ReadLine();

        if (password != confirmPassword)
        {
            do
            {
                Console.WriteLine("Passwords do not match. Please try again.");
                confirmPassword = Console.ReadLine();
            } while  (password != confirmPassword);
        }

        Guest guest = new Guest(firstName, lastName, email, phoneNumber, password);
        Home.ChangeGuest(guest);
        Console.WriteLine($"Account created successfully. Thank you for signing up {guest.FirstName}!");
        System.Console.WriteLine("Make sure you log in");
        Home.Options();   
    }
}
