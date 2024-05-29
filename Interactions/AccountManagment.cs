using System.Threading;

// nieuwe updateee !!! user kan zijn wachtwoord veranderen changes in: accountsmanagment, homeoptions, usersaccess
static class AccountManagment
{
    public static void ChangePassword()
    {
        System.Console.WriteLine("(At any time type 'Q' to go back)");
        Console.WriteLine("Email address:");
        string email = Console.ReadLine();
        if (email.ToLower() == "q"){
         return;
        }
        var user = UsersAccess.GetUser(email);

        if (user == null)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("User not found. Please try again."); Console.ResetColor();
            return;
        }

        Console.WriteLine("Current password:");
        string currentPassword = Console.ReadLine();
        if (currentPassword.ToLower() == "q"){
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
            Console.WriteLine("Enter new password:");
            newPassword = Console.ReadLine();
        if (newPassword.ToLower() == "q"){
            return;
        }
        } while (string.IsNullOrEmpty(newPassword));

        Console.WriteLine("Confirm new password:");
        string confirmNewPassword = Console.ReadLine();
        if (confirmNewPassword.ToLower() == "q"){
            return;
        }
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
                if (ManagerCode.ToLower() == "q"){
                return;
                }
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
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid email or password. Please try again."); Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Logged out successfully."); Console.ResetColor();
        }
        else if (Home.ManagerLoggedIn)
        {
            Console.WriteLine("Logging out manager...");
            Home.ManagerLoggedIn = false;
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
        } while (!CheckUserInfo.IsValidEmail(email));

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
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Passwords do not match. Please try again."); Console.ResetColor();
                confirmPassword = Console.ReadLine();
            } while  (password != confirmPassword);
        }

        Guest guest = new Guest(firstName, lastName, email, phoneNumber, password);
        Home.ChangeGuest(guest);
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Account created successfully. Thank you for signing up {guest.FirstName}!"); Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Make sure you log in"); Console.ResetColor();
        Home.Options();   
    }
}
