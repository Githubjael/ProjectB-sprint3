using System.Threading;

using System.Threading;

// nieuwe updateee dont forget to update had vergeten exceptions erby toe te voegen !!! 
static class AccountManagment
{
    public static void ChangePassword()
    {
        Console.WriteLine("Email address:");
        string email = Console.ReadLine();
        if (email.ToLower() == "q")
        {
            return;
        }

        var user = UsersAccess.GetUser(email);

        if (user == null)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("User not found. Please try again."); Console.ResetColor();
            Console.ResetColor();
            return;
        }

        Console.WriteLine("Current password:");
        string currentPassword = Console.ReadLine();
        if (currentPassword.ToLower() == "q")
        {
            return;
        }

        if (user.Password != currentPassword)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Incorrect current password. Please try again."); Console.ResetColor();
            return;
        }

        string newPassword;
        do
        {
            Console.WriteLine("Enter a new valid password (8-20 characters):");
            newPassword = Console.ReadLine();
            if (newPassword.ToLower() == "q")
            {
                return;
            }

            if (newPassword.Length < 8 || newPassword.Length > 20)
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Password should be at least 8 characters and not more than 20 characters."); Console.ResetColor();
            }
        } while (string.IsNullOrEmpty(newPassword) || newPassword.Length < 8 || newPassword.Length > 20);


        Console.WriteLine("Confirm new password:");
        string confirmNewPassword = Console.ReadLine();

        if (newPassword != confirmNewPassword)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Passwords do not match. Please try again."); Console.ResetColor();
            return;
        }

        user.Password = newPassword;
        UsersAccess.UpdateUser(user);

        var updatedUser = UsersAccess.GetUser(email);
        if (updatedUser != null && updatedUser.Password == newPassword)
        {
            Console.WriteLine(". . . . .\n");
            System.Threading.Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Password changed successfully."); Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Password change failed. Please try again."); Console.ResetColor();
        }
    }


    public static void LogIn()
    {
        // Log in as manager or guest
        while (!Home.IsLoggedIn && !Home.ManagerLoggedIn)
        {
            System.Console.WriteLine("(At any time type 'Q' to go back)");
            Console.WriteLine("Email address:");
            string email = Console.ReadLine();
            if (email.ToLower() == "q")
            {
                return;
            }
            Console.WriteLine("Password:");
            string password = Console.ReadLine();
            if (password.ToLower() == "q")
            {
                return;
            }
            var user = UsersAccess.GetUser(email);
            Manager manager = ManagerAccess.ReadFromJson()[0];
            if (user != null && user.Password == password)
            {
                Console.WriteLine(". . . . .\n");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged in successfully!"); Console.ResetColor();
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
                if (ManagerCode.ToLower() == "q")
                {
                    return;
                }
                if (ManagerCode == manager.EmployeeCode)
                {
                    Console.WriteLine(". . . . .\n");
                    System.Threading.Thread.Sleep(2500);
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged in successfully!"); Console.ResetColor();
                    Home.ManagerLoggedIn = true;
                    Home.IsLoggedIn = false;
                    Home.Options();
                }
                else{ Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Manager Code."); Console.ResetColor();}
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid email or password. Please try again."); Console.ResetColor();
            }
        }
    }

    public static void LogOut()
    {
        if (Home.IsLoggedIn)
        {
            Home.IsLoggedIn = false;
            Home.guestName = null;
            Home.guestEmail = null;
            System.Console.WriteLine(". . . . .\n");
            System.Threading.Thread.Sleep(2500);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged out successfully."); Console.ResetColor();
        }
        else if (Home.ManagerLoggedIn)
        {
            Home.ManagerLoggedIn = false;
            System.Console.WriteLine(". . . . .\n");
            System.Threading.Thread.Sleep(2500);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged out successfully."); Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("No user logged in."); Console.ResetColor();
        }

        Home.Options();

    }

        public static void SignUp()
    {
        string firstName = PersonalDetails.AskFirstName();

        string lastName = PersonalDetails.AskLastName();

        string email = PersonalDetails.AskEmailAddress();

        string phoneNumber = PersonalDetails.AskPhoneNumber();

        string password;
        do
        {
            Console.WriteLine("Enter a valid password (8-20 characters):");
            password = Console.ReadLine();
            if (password.ToLower() == "q")
            {
                return;
            }

            if (password.Length < 8 || password.Length > 20)
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Password should be at least 8 characters and not more than 20 characters."); Console.ResetColor();
            }
        } while (password.Length < 8 || password.Length > 20);

        Console.WriteLine("Confirm your password:");
        string confirmPassword = Console.ReadLine();
        if (confirmPassword.ToLower() == "q")
        {
            return;
        }

        if (password != confirmPassword)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Passwords do not match. Please try again."); Console.ResetColor();
                confirmPassword = Console.ReadLine();
            } while (password != confirmPassword);
        }

        Guest guest = new Guest(firstName, lastName, email, phoneNumber, password);
        Home.ChangeGuest(guest);
        Console.WriteLine(". . . . .\n");
        System.Threading.Thread.Sleep(2000);
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Account created successfully. Thank you for signing up, {guest.FirstName}!"); Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Make sure you log in"); Console.ResetColor();
        Home.Options();
    }
}
