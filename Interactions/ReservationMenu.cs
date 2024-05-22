using System;
using System.Collections.Generic;

public static class ReservationMenu
{
    public static void Options()
    {
        while (true)
        {
            Console.WriteLine("[H]: Home");
            Console.WriteLine("[M]: Make reservation");
            Console.WriteLine("[CR]: Cancel reservation");
            Console.WriteLine("[V]: View reservation history");

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
                    if (Home.IsLoggedIn && !Home.ManagerLoggedIn)
                    {
                        Console.WriteLine("Your upcoming reservations:");
                        var now = DateTime.Now;
                        var reservations = ReservationLogic.FindReservationsByEmail(Home.guestEmail)
                                                            .Where(r => DateTime.Parse(r.Date + " " + r.Time) > now);

                        if (reservations.Any())
                        {
                            foreach (var reservation in reservations)
                            {
                                Console.WriteLine($"Guest ID: {reservation.GuestID}");
                                Console.WriteLine($"Name: {reservation.FirstName} {reservation.LastName}");
                                Console.WriteLine($"Date: {reservation.Date}");
                                Console.WriteLine($"Time: {reservation.Time}");
                                Console.WriteLine();
                            }

                            string date;
                            DateTime parsedDate;
                            do
                            {
                                Console.WriteLine("On which date would you like to cancel your reservation? (dd-MM-yyyy)");
                                date = Console.ReadLine();
                            }
                            while (!DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate));

                            string time;
                            TimeSpan parsedTime;
                            do
                            {
                                Console.WriteLine("Enter the time of your reservation: (hh:mm)");
                                time = Console.ReadLine();
                            }
                            while (!TimeSpan.TryParseExact(time, "hh\\:mm", null, System.Globalization.TimeSpanStyles.None, out parsedTime));

                            ReservationLogic.CancelReservationByAcc(Home.guestEmail, date, time);
                        }
                        else
                        {
                            Console.WriteLine("No upcoming reservations found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You need to log in to cancel a reservation.");
                    }
                    Options();
                    return;


                case "V":
                    // View reservation history
                    if (Home.IsLoggedIn && !Home.ManagerLoggedIn)
                    {
                        Console.WriteLine("Your reservation history:");
                        var reservations = ReservationLogic.FindReservationsByEmail(Home.guestEmail);
                        if (reservations != null && reservations.Count > 0)
                        {
                            foreach (var reservation in reservations)
                            {
                                Console.WriteLine($"Guest ID: {reservation.GuestID}");
                                Console.WriteLine($"Name: {reservation.FirstName} {reservation.LastName}");
                                Console.WriteLine($"Date: {reservation.Date}");
                                Console.WriteLine($"Time: {reservation.Time}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No reservations found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You need to log in to view your reservation history.");
                    }
                    Options();
                    return;
                case "H":
                    Home.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
