using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class Home
{
    public static string IsLoggedIn { get; private set; }
    private static string _name = "Home";
    private static List<Person> Users = new List<Person>();

    public static string Name
    {
        get => _name;
    }

    public static void Options()
    {
        //restaurant info print here (make into json)
        Console.WriteLine("[M]: Menu");
        Console.WriteLine("[R]: Reservation");
        Console.WriteLine("[RV]: Review");
        Console.WriteLine("[C]: Contact");
        Console.WriteLine("[L]: Log in");
        Console.WriteLine("[S]: Sign up");

        while (true)
        {
            string UserChoice = Console.ReadLine().ToUpper();

            switch (UserChoice)
            {
                case "L":
                    LogIn();
                    break;
                case "S":
                    SignUp();
                    break;
                case "M":
                    Menu.Options();
                    return;
                case "R":
                    Reservation.Options();
                    return;
                case "RV":
                    Reviews.Options();
                    return;
                case "C":
                    Contact.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
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


        var newUser = new Guest(firstName, lastName, email, phoneNumber, password);
        UsersAccess.SaveUser(newUser);

        Console.WriteLine("Account created successfully. You can now log in.");
    }

    public static void LogIn()
    {
        bool isLoggedIn = false;

        while (!isLoggedIn)
        {
            Console.WriteLine("Email address:");
            string email = Console.ReadLine();

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            var user = UsersAccess.GetUser(email);
            if (user != null && user.Password == password)
            {
                IsLoggedIn = "true";
                Console.WriteLine("Logged in successfully!");
                isLoggedIn = true; 
            }
            else
            {
                Console.WriteLine("Invalid email or password. Please try again.");
            }
        }
    }

}
