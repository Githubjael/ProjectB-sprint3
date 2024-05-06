using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

public static class Home
{
    public static bool IsLoggedIn { get; private set; }
    public static bool ManagerLoggedIn {get; private set;}
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
        // Console.Clear();
        //restaurant info print here (make into json)
        Manager manager = ManagerAccess.ReadFromJson()[0];
        System.Console.WriteLine();
        System.Console.WriteLine(new string("𓌉◯ 𓇋 Jake's Restaurant 𓌉◯ 𓇋"));
        string streep = "";
        string shortIntro = $"{Reviews.AverageRating()} - €{Menu.MaxPrice()} and lower - 10:00-22:00";
        foreach(char x in shortIntro)
        {
            streep += "-";
        }
        System.Console.WriteLine(streep);
        System.Console.WriteLine(shortIntro);
        System.Console.WriteLine(streep);
        if (IsLoggedIn || ManagerLoggedIn)
        {
            System.Console.WriteLine($"Welkom back, {guestName}!");
        }
        Console.WriteLine("[M]: Menu");
        Console.WriteLine("[R]: Reservation");
        Console.WriteLine("[RV]: Review");
        Console.WriteLine("[C]: Contact");
        if (!IsLoggedIn && !ManagerLoggedIn){
        Console.WriteLine("[L]: Log in");
        Console.WriteLine("[S]: Sign up");
        }

        while (true)
        {
            string UserChoice = Console.ReadLine().ToUpper();

            switch (UserChoice)
            {
                case "L":
                    // Console.Clear();
                    LogIn();
                    break;
                case "S":
                    // Console.Clear();
                    SignUp();
                    break;
                case "M":
                    // Console.Clear();
                    Menu.Options();
                    return;
                case "R":
                    if (ManagerLoggedIn)
                    {
                    // Console.Clear();
                    manager.ReservationOptions();
                    }
                    else{
                    // Console.Clear();
                    Reservation.Options();
                    }
                    return;
                case "RV":
                if (ManagerLoggedIn)
                    {
                    // Console.Clear();
                    manager.ReviewOptions();
                    }
                    else
                    {
                        // Console.Clear();
                        Reviews.Options();
                    }
                    return;
                case "C":
                    // Console.Clear();
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


        _guest = new Guest(firstName, lastName, email, phoneNumber, password);
        UsersAccess.SaveUser(_guest);

        Console.WriteLine($"Account created successfully. Thank you for signing up {_guest.FirstName}!");
        System.Console.WriteLine("Make sure you log in");
        Options();
    }


    public static void LogIn()
    {
        // Log in als manager of log in als guest bla bla bla
        while (!IsLoggedIn || !ManagerLoggedIn)
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
                IsLoggedIn = true;
                guestEmail = user.EmailAddress;
                guestName = user.FirstName;
                ManagerLoggedIn = false;
                Console.WriteLine();
                Console.WriteLine();
                Options(); 
            }
            else if (user == null && manager.EmailAddress == email && manager.Password == password)
            {
                System.Console.WriteLine("Manager code:");
                string ManagerCode = Console.ReadLine();
                if (ManagerCode == manager.EmployeeCode)
                {
                    System.Console.WriteLine($"Welcome back, {manager.FirstName}");
                    ManagerLoggedIn = true;
                    IsLoggedIn = false;
                    Options();
                }
            }
            else
            {
                Console.WriteLine("Invalid email or password. Please try again.");
            }
    }
    }
}
