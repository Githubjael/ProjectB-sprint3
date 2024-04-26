using System.Security.Cryptography.X509Certificates;

public class ReservationLogic
{
    public static List<ReservationDataModel> _reservation = new List<ReservationDataModel>() { };

    //  voor nu adden we alleen maar reservations naar de prive lijst
    public static void AddReservationFromJson()
    {
        if (ReservationDataAccess.ReadFromJson() != null)
        {
            foreach (ReservationDataModel reservation in ReservationDataAccess.ReadFromJson())
            {
                _reservation.Add(reservation);
            }
        }
    }

    //  voor nu adden we alleen maar reservations naar de prive lijst
    public static void AddReservationToList(ReservationDataModel reservation)
    {
        _reservation.Add(reservation);
        _reservation.Sort();
        ReservationDataAccess.WriteToJson(_reservation);
    }
    //  en deze lijst sturen we op een of andere manier naar json
    //  We roepen een method vanuit ReservationDataAccess (de write to json method)

    // Oke guys hier komt een method die een lijst maakt met volgeboekte datums zodat de gast weet welke dat zijn 

    public static List<string> VolGeboekteDatums()
    {
        // Voor reservation in reservations
        List<string> VolGeboekteDatums = new (){};
        string Datum;
        foreach (ReservationDataModel reservation in _reservation)
        {
            Datum = reservation.Date;
            if (ReservedTable.GetTimes(Datum).Count == 0)
            {
                if(!VolGeboekteDatums.Contains(Datum))
                {
                VolGeboekteDatums.Add(Datum);
                }
            }
        }
        return VolGeboekteDatums;
    }

    public static List<string> GeboekteDatums()
    {
        List<string> FullyBookedDates = VolGeboekteDatums();
        List<string> Datums = new(){};
        List<string> Times = new(){
            "10:00", "17:00", "22:00"
        };
        List<string> Booked = new(){};
        foreach(ReservationDataModel reservation in _reservation)
        {
            if (!Datums.Contains(reservation.Date))
            {
                Datums.Add(reservation.Date);
            }
        }
        foreach(string Datum in Datums)
        {
            foreach(string Time in Times)
            {
                int TableCount = 16;
            foreach(ReservationDataModel reservation in _reservation)
            {
                if (reservation.Date == Datum && reservation.Time == Time)
                {
                    TableCount -= reservation.Tables.Count;
                }
            }
            if (TableCount > 1 && TableCount != 0 && TableCount != 16)
            {
            Booked.Add($"{Datum}, {Time} ({TableCount} tables left)");
            }
            else if (TableCount == 1)
            {
            Booked.Add($"{Datum}, {Time} ({TableCount} table left)");
            }
            else if (TableCount == 0 && !FullyBookedDates.Contains(Datum))
            {
                Booked.Add($"{Datum}, {Time} (fully booked)");
            }
            }
        }
        return Booked;
    }

    public static void PrintAllReservations()
    {
        // See all reservations
        for(int i = 0; i < _reservation.Count; i++)
        {
            ReservationDataModel reservation = _reservation[i];
            Console.WriteLine("===============");
            Console.WriteLine($"Reservation: {i + 1}");
            PrintReservation(reservation);
        }
    }

    public static void PrintReservationsBasedOnDate(string Date)
    {
        for(int i = 0; i < _reservation.Count; i++)
        {
            ReservationDataModel reservation = _reservation[i];
            if (reservation.Date == Date)
            {
            Console.WriteLine("===============");
            Console.WriteLine($"Reservation: {i + 1}");
            PrintReservation(reservation);
            }
        }
    }

    public static void PrintReservationBasedOnTime(string Date, string Time)
    {
        for(int i = 0; i < _reservation.Count; i++)
        {
            ReservationDataModel reservation = _reservation[i];
            if (reservation.Date == Date && reservation.Time == Time)
            {
            Console.WriteLine("===============");
            Console.WriteLine($"Reservation: {i + 1}");
            PrintReservation(reservation);
            } 
        }
    }

    public static void PrintReservation(ReservationDataModel reservation)
    {
        System.Console.WriteLine($"Date: {reservation.Date}");
        System.Console.WriteLine($"Time: {reservation.Time}");
        System.Console.WriteLine($"Guest id: {reservation.GuestID}");
        System.Console.WriteLine($"Name: {reservation.FirstName} {reservation.LastName}");
        System.Console.WriteLine($"Email address: {reservation.EmailAddress}");
        foreach(Table table in reservation.Tables)
        {
            System.Console.WriteLine($"Table id: {table.ID}");
        }
    }

    public static void CancelReservation(int guestID)
    {
        ReservationDataModel reservationToRemove = FindReservationByGuestID(guestID);

        if (reservationToRemove != null)
        {
            DateTime now = DateTime.Now;
            DateTime reservationDateTime;

            if (DateTime.TryParse(reservationToRemove.Date + " " + reservationToRemove.Time, out reservationDateTime))
            {
                // ik controleer hoelang er nog voor de reservatie is/ dus of er nog minder dan 2 uur is / want dan is het annuleren niet meer mogelijk !
                if ((reservationDateTime - now).TotalHours < 2)
                {
                    Console.WriteLine("Sorry, you can't cancel the reservation as it's less than 2 hours before/Past the reservation time. ");
                    return;
                }

                // de rrservatie wordt geanulleerd 
                _reservation.Remove(reservationToRemove);
                ReservationDataAccess.WriteToJson(_reservation);
                Console.WriteLine("Your Reservation is cancelled.");
            }
            else
            {
                Console.WriteLine("Error converting reservation time.");
            }
        }
        else
        {
            Console.WriteLine("Reservation is not found.");
        }
    }




    //  korte methode om ff de reservatie op basis van guest ID te vinden.
    public static ReservationDataModel FindReservationByGuestID(int guestID)
    {
        foreach (ReservationDataModel reservation in _reservation)  //deze loop gaat door de reservaties in de lijst , totdat hij de reservatie tegenkomt die gebonden is aan de guestID die hij uit de parameter krijgt.
        {
            if (reservation.GuestID == guestID)
            {
                return reservation;
            }
        }
        return null; //null == geen reservatie gevonden
    }


    // check of tafel bezet is
public static bool CheckReservedTable(string ID, string Date, string Time)
{
    foreach (ReservationDataModel reservation in _reservation)
    {
        foreach (var table in reservation.Tables)
        {
            if (table.ID == ID && reservation.Date == Date && reservation.Time == Time)
            {
                return true;
            }
        }
    }
    return false;
}
   public static List<Table> SwitchIfNull(int type)
    {
        List<Table> tables = new List<Table>() { };
        if (type == 2)
        {
            // 6 tafels geven als ook vol
            var found = ReservedTable.TableTracker.Find(x => x.Type == 4 && x.Reserved == false);
            if (!(found is null))
            {
                found.IsReserved();
                tables.Add(found);
            }
            else
            {
                found = ReservedTable.TableTracker.Find(x => x.Type == 6 && !x.Reserved);
                if (!(found is null))
                {
                    found.IsReserved();
                    tables.Add(found);
                }
            }
        }
        else if (type == 4)
        {
            var found = ReservedTable.TableTracker.Find(x => x.Type == 6 && x.Reserved == false);
            if (found is null)
            {
                List<int> tabletypes = new List<int>() { 2, 2 };
                tables = new List<Table>() { };
                foreach (int types in tabletypes)
                {
                    found = ReservedTable.TableTracker.Find(x => x.Type == types && x.Reserved == false);
                    if (!(found is null))
                    {
                        found.IsReserved();
                        tables.Add(found);
                    }
                }
                // ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, tables);
                // AddReservationToList(Reservation);
            }
            else
            {
                found.IsReserved();
                tables.Add(found);
            }
        }
        else if (type == 6)
        {
            List<int> tabletypes = new List<int>() { 2, 4};
            tables = new List<Table>();
            foreach (int types in tabletypes)
            {
                var found = ReservedTable.TableTracker.Find(x => x.Type == types && x.Reserved == false);
                if (!(found is null))
                {
                    found.IsReserved();
                    tables.Add(found);
                }
            }
            if (tables.Count != 2)
            {
                tabletypes = new List<int>() { 2, 2, 2 };
                tables = new List<Table>();
                foreach (int types in tabletypes)
                {
                    var found = ReservedTable.TableTracker.Find(x => x.Type == types && x.Reserved == false);
                    if (!(found is null))
                    {
                        found.IsReserved();
                        tables.Add(found);
                    }
                }
                // ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, tables);
                // AddReservationToList(Reservation);
            }
        }
        return tables;
        }

    // het doel van deze static function is om de table type te veranderen als er geen unreserved table
    // beschickbaar zijn
    
}
