using System.Globalization;

class Reservation : Page
{
    public static List<int> unavailableGuestIDs = new List<int>(); // ik een lijst om de gebruikte Guests IDs op te slaan
    private static Random random = new Random();
    public static int GenerateRandomGuestID()
    {
        int guestID;
        do
        {
            guestID = random.Next(1, 200);
        } while (unavailableGuestIDs.Contains(guestID));

        unavailableGuestIDs.Add(guestID);
        return guestID;
    }
    public override string Name => "Reservation";

    public override void Contents()
    {
        throw new NotImplementedException();
    }

    public static void Options() => ReservationMenu.Options();

    public static void MakeReservation()
    {
        ReservationDataAccess.ReadFromJson();
        Messages.ReservationGreetings();
        if(!Home.IsLoggedIn)
        {
            string FirstName = PersonalDetails.AskFirstName();
            if (FirstName == null){
                return;
            }
            string LastName = PersonalDetails.AskLastName();
            if (LastName == null){
                return;
            }
            string Email = PersonalDetails.AskEmailAddress();
            if (Email == null){
                return;
            }
            string PhoneNumber = PersonalDetails.AskPhoneNumber();
            if (PhoneNumber == null){
                return;
            }

            List<string> BookedDates = ReservationLogic.VolGeboekteDatums();
            DateTime Date = PersonalDetails.AskDate(BookedDates);
            if (Date == DateTime.MinValue){
                return;
            }
            string TimeSlot = PersonalDetails.AskTimeSlot(Date);
            if (TimeSlot == null){
                return;
            }
            int Guests = PersonalDetails.AskAmountOfGuests();
            if (Guests == 0){
                return;
            }
            List<Table> Tables;
            if (Guests > 6)
            {
                Tables =  ReservedTable.AssignTables(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
                Console.Clear();
            }
            else{
                Tables = ReservedTable.AssignTable(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            int GuestID = GenerateRandomGuestID();
            ReservationDataModel Reservation = new(GuestID, FirstName, LastName, PhoneNumber, Email, Date.ToString("dd-MM-yyyy"), TimeSlot, Tables);
            ReservationLogic.AddReservationToList(Reservation);
            Messages.Thanking4Reservation(GuestID);
        }
        else
        {
            Guest guest = UsersAccess.GetUser(Home.guestEmail);
            List<string> BookedDates = ReservationLogic.VolGeboekteDatums();
            DateTime Date = PersonalDetails.AskDate(BookedDates);
            int Guests = PersonalDetails.AskAmountOfGuests();
            string TimeSlot = PersonalDetails.AskTimeSlot(Date);
            List<Table> Tables;
            if (Guests > 6)
            {
                Tables =  ReservedTable.AssignTables(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            else{
                Tables = ReservedTable.AssignTable(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            int GuestID = GenerateRandomGuestID();
            ReservationDataModel Reservation = new(GuestID, guest.FirstName, guest.LastName, guest.phoneNumber, guest.EmailAddress, Date.ToString("dd-MM-yyyy"), TimeSlot, Tables);
            ReservationLogic.AddReservationToList(Reservation);
            Messages.Thanking4Reservation(Reservation.GuestID);
        }
    }
    public static void CancelReservation(int guestID)
    {
        ReservationLogic.CancelReservation(guestID); 
    }
}

