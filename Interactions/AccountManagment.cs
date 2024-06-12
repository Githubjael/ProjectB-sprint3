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
            Console.WriteLine("User not found. Please try again.");
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("User not found. Please try again.");
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
            Console.WriteLine("Incorrect current password. Please try again.");
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
                Console.WriteLine("Password should be at least 8 characters and not more than 20 characters.");
            }
        } while (string.IsNullOrEmpty(newPassword) || newPassword.Length < 8 || newPassword.Length > 20);


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
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged in successfully!"); Console.ResetColor();
                    Console.WriteLine($"Welcome back, {manager.FirstName}");
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
            System.Console.WriteLine(". . . .");
            System.Threading.Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged out successfully."); Console.ResetColor();
        }
        else if (Home.ManagerLoggedIn)
        {
            Home.ManagerLoggedIn = false;
            System.Console.WriteLine(". . . .");
            System.Threading.Thread.Sleep(1500);
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
        string firstName;
        do
        {
            System.Console.WriteLine("(At any time type 'Q' to go back)");
            Console.WriteLine("Enter a valid first name (2-20 letters):");
            firstName = Console.ReadLine();
            if (firstName.ToLower() == "q")
            {
                return;
            }
            if (firstName.Length < 2 || firstName.Length > 20 || !CheckUserInfo.IsValidName(firstName))
            {
                Console.WriteLine("First name should be between 2 and 20 characters and consist of only letters.");
            }
        } while (firstName.Length < 2 || firstName.Length > 20 || !CheckUserInfo.IsValidName(firstName));

        string lastName;
        do
        {
            Console.WriteLine("Enter a valid last name (2-20 letters):");
            lastName = Console.ReadLine();
            if (lastName.ToLower() == "q")
            {
                return;
            }
            if (lastName.Length < 2 || lastName.Length > 20 || !CheckUserInfo.IsValidName(lastName))
            {
                Console.WriteLine("Last name should be between 2 and 20 characters and consist of only letters.");
            }
        } while (lastName.Length < 2 || lastName.Length > 20 || !CheckUserInfo.IsValidName(lastName));


        string email;
        do
        {
            Console.WriteLine("Enter a valid email (must start with a letter):");
            email = Console.ReadLine();
            if (email.ToLower() == "q")
            {
                return;
            }
            if (!CheckUserInfo.IsValidEmail(email))
            {
                Console.WriteLine("Email should be valid and start with a letter.");
            }
        } while (!CheckUserInfo.IsValidEmail(email));

        string phoneNumber;
        do
        {
            Console.WriteLine("Enter a valid phone number:");
            phoneNumber = Console.ReadLine();
            if (phoneNumber.ToLower() == "q")
            {
                return;
            }
        } while (!CheckUserInfo.IsValidPhoneNumber(phoneNumber));

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
                Console.WriteLine("Password should be at least 8 characters and not more than 20 characters.");
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
                Console.WriteLine("Passwords do not match. Please try again.");
                confirmPassword = Console.ReadLine();
            } while (password != confirmPassword);
        }

        Guest guest = new Guest(firstName, lastName, email, phoneNumber, password);
        Home.ChangeGuest(guest);
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Account created successfully. Thank you for signing up, {guest.FirstName}!"); Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Make sure you log in"); Console.ResetColor();
        Home.Options();
    }
}
