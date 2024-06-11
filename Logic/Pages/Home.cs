using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

public static class Home
{
    public static bool IsLoggedIn { get; set; }
    public static bool ManagerLoggedIn {get; set;}
    public static string guestName { get; set; }
    public static string guestEmail { get; set;}
    public static string Name => "Home";

    // Methodes revereren naar methodes in de interactie laag
    public static List<string> ShowTimeSlots() => TimeSlots.ReadFromJson();
    public static void ChangeTimeSlots(List<string> Timeslots) => TimeSlots.WriteToJson(Timeslots);
    public static void Options() => HomeOptions.Options();
    public static void SignUp() => AccountManagment.SignUp();

    public static void LogOut() => AccountManagment.LogOut();

    public static void LogIn() => AccountManagment.LogIn();
    public static void ChangeGuest(Guest guest) => UsersAccess.SaveUser(guest);
}
