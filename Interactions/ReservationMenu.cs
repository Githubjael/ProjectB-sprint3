static class ReservationMenu
{
    public static void Options()
    {
        while(true){
        Console.WriteLine("[H]: Home");
        Console.WriteLine("[M]: Make reservation");
        Console.WriteLine("[CR]: Cancel reservation");

            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "M":
                    // Make reservation
                    Reservation.MakeReservation();
                    Options();
                    return;
                case "CR":
                    // Cancel reservation
                    System.Console.WriteLine("Enter your guest ID"); // voorbeeld guestID deze wordt normaal ingevoerd in program maar dat zien we later wel
                    try{
                    int guestID = Convert.ToInt32(Console.ReadLine());
                    ReservationLogic.CancelReservation(guestID);
                    }
                    catch (Exception ex){
                        System.Console.WriteLine("Please enter a valid number");
                    }
                    Options();
                    return;
                case "H":
                    Home.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    Options();
                    break;
            }
        }
    }
}
