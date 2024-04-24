using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

public static class Home
{
    public static bool IsLoggedIn { get; private set; }
    private static string _name = "Home";
    private static List<Guest> Users = new List<Guest>();
    public static string guestName { get; private set; }
    public static string guestEmail { get; private set;}
    private static Guest _guest;
    public static string Name
    {
        get => _name;
    }

    public static void Options()
    {
        //restaurant info print here (make into json)
        if (IsLoggedIn)
        {
            System.Console.WriteLine($"Welkom back, {guestName}!");
        }
        Console.WriteLine("[M]: Menu");
        Console.WriteLine("[R]: Reservation");
        Console.WriteLine("[RV]: Review");
        Console.WriteLine("[C]: Contact");
        Console.WriteLine("[S]: Sign up");
        Console.WriteLine("[L]: Log in");
        Console.WriteLine("[LO]: Log out");

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
                case "LO":
                    LogOut();
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


        _guest = new Guest(firstName, lastName, email, phoneNumber, password);
        UsersAccess.SaveUser(_guest);

        Console.WriteLine($"Account created successfully. Thank you for signing up {_guest.FirstName}!");
        System.Console.WriteLine("Make sure you log in");
        Options();
    }


    public static void LogOut()
    {
        string choice;
        while (IsLoggedIn)
        {
            Console.WriteLine("Do you want to log uit? (y/n)");
            choice = Console.ReadLine().ToLower();
            if (choice == "y")
            {
                IsLoggedIn = false;
                Options();
            }
            else if (choice == "n")
            {
                IsLoggedIn = true;
                Options();
            }
            else
            {
                IsLoggedIn = true;
                Options();
            }
        }
    }


    public static void LogIn()
    {
        // Log in als manager of log in als guest bla bla bla
        while (!IsLoggedIn)
        {
            Console.WriteLine("Email address:");
            string email = Console.ReadLine();

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            var user = UsersAccess.GetUser(email);
            if (user != null && user.Password == password)
            {
                Console.WriteLine("Logged in successfully!");
                IsLoggedIn = true;
                guestEmail = user.EmailAddress;
                guestName = user.FirstName;
                Console.WriteLine();
                Console.WriteLine();
                Options(); 
            }
            else
            {
                Console.WriteLine("Invalid email or password. Please try again.");
            }
    }

    }
}

    }
}
