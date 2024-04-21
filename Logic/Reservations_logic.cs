using System.Security.Cryptography.X509Certificates;

public class ReservationLogic
{
    public static List<ReservationDataModel> _reservation = new List<ReservationDataModel>(){};

     //  voor nu adden we alleen maar reservations naar de prive lijst
    public static void AddReservationFromJson()
    {
        if (ReservationDataAccess.ReadFromJson() != null){
        foreach(ReservationDataModel reservation in ReservationDataAccess.ReadFromJson())
        {
            _reservation.Add(reservation);
        }
    }
    }

    //  voor nu adden we alleen maar reservations naar de prive lijst
    public static void AddReservationToList(ReservationDataModel reservation)
    {
        _reservation.Add(reservation);
        ReservationDataAccess.WriteToJson(_reservation);
    }
    //  en deze lijst sturen we op een of andere manier naar json
    //  We roepen een method vanuit ReservationDataAccess (de write to json method)


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
        foreach(ReservationDataModel reservation in _reservation)
        {
            for (int i = 0; i < reservation.Tables.Count; i++){ // Waarom gaat ie niet door de lijst met meer dan 1 Tafel?
                if(reservation.Tables[i].ID == ID && reservation.Date == Date && reservation.Time == Time)
                {
                    return true;
                }
            }
        }
        return false;
    }
    // het doel van deze static function is om de table type te veranderen als er geen unreserved table
    // beschickbaar zijn
    public static void SwitchIfNull(Table foundnull, int type, int guestID, string FirstName, string LastName, string PhoneNumber, string EmailAddress, string ChosenDayFinal, string ChosenMonthFinal, int ChosenYear, string ChosenTime){
        if (foundnull is null){
            List<Table> tables = new List<Table>(){};
            if (type == 2){
                var found = ReservedTable.TableTracker.Find(x=> x.Type == 4 && x.Reserved == false);
                if(!(found is null)){
                    found.IsReserved();
                    tables.Add(found);
                }
            }
            else if (type == 4){
                var found = ReservedTable.TableTracker.Find(x=> x.Type == 6 && x.Reserved == false);
                if (found is null){
                    List<int> tabletypes = new List<int>(){ 2, 4};
                    tables = new List<Table>(){};
                    foreach (int types in tabletypes){
                        found = ReservedTable.TableTracker.Find(x=> x.Type == types && x.Reserved == false);
                        if (!(found is null)){
                            found.IsReserved();
                            tables.Add(found);
                        }
                    }
                    // ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, tables);
                    // AddReservationToList(Reservation);
                }
            }
            else if (type == 6){
                List<int> tabletypes = new List<int>(){ 2, 4};
                tables = new List<Table>();
                foreach (int types in tabletypes){
                    var found = ReservedTable.TableTracker.Find(x=> x.Type == types && x.Reserved == false);
                    if (!(found is null)){
                        found.IsReserved();
                        tables.Add(found);
                    }
                }
                if (tables.Count != 2){
                    tabletypes = new List<int>(){ 2, 2, 2};
                    tables = new List<Table>();
                    foreach (int types in tabletypes){
                    var found = ReservedTable.TableTracker.Find(x=> x.Type == types && x.Reserved == false);
                    if (!(found is null)){
                        found.IsReserved();
                        tables.Add(found);
                    }
                }
                // ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, tables);
                // AddReservationToList(Reservation);
                }
            }
            ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, tables);
            AddReservationToList(Reservation);
        }
        else{
            Console.WriteLine("Error in adjusting tables for reservation.");
        }
    }
}
