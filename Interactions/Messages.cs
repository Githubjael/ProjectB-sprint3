static class Messages
{

    public static void Greeting()
    {
        if (Home.IsLoggedIn)
            System.Console.WriteLine($"Welkom back, {Home.guestName}!");
        else if(Home.ManagerLoggedIn)
            System.Console.WriteLine($"Welkom, Jane");
    }
    public static void ReservationGreetings()
    {
        if(Home.IsLoggedIn)
        {
            System.Console.WriteLine($"Hi {Home.guestName}! please provide us with the following details:");
        }
        else{
            System.Console.WriteLine("We are honored by your interest in booking at our restaurant!");
            System.Console.WriteLine("Please answer the following questions:");
        }  
    }

    public static void Thanking4Reservation(int GuestID)
    {
        System.Console.WriteLine($"Thank you for choosing to book with us! Your guest ID is {GuestID}.\nIf you dont have an account yet and need to cancel your reservation, please provide this guest ID for easy processing. We look forward to serving you!");
    }
}
