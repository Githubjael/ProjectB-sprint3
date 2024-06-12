using System;
using System.Collections.Generic;

public static class ReservationMenu
{
    public static void Options()
    {
        while (true)
        {
            Console.WriteLine("[1]: Home");
            Console.WriteLine("[2]: Make reservation");
            Console.WriteLine("[3]: Cancel reservation");
            Console.WriteLine("[4]: View reservation history");

            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "2":
                    // Make reservation
                    Reservation.MakeReservation();
                    Options();
                    return;
                case "3":
                    

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


                case "4":
                    // View reservation history
                    if (Home.IsLoggedIn && !Home.ManagerLoggedIn)
                    {
                        Console.WriteLine("Your reservation history:");
                        var reservations = ReservationLogic.FindReservationsByEmail(Home.guestEmail);
                        if (reservations != null && reservations.Count > 0)
                        {
                            foreach (var reservation in reservations)
                            {
                                Console.WriteLine($"Name: {reservation.FirstName} {reservation.LastName}");
                                Console.WriteLine($"Date: {reservation.Date}");
                                Console.WriteLine($"Time: {reservation.Time}");
                                if (reservation.Preorder != null){
                                System.Console.WriteLine($"Pre-order:");
                                foreach(var dish in reservation.Preorder.Dishes)
                                {
                                    System.Console.WriteLine($"- {dish}, ${PreOrdering.menuItems.Find(x => x.Name == dish).Price}");
                                }
                                Console.WriteLine();
                            }
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
                case "1":
                    Home.Options();
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
