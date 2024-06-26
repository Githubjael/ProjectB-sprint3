using System.Globalization;

class Reservation
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
    
    public static void Options() => ReservationMenu.Options();

    public static void MakeReservation()
    {
        ReservationDataAccess.ReadFromJson();
        Messages.ReservationGreetings();
        if(!Home.IsLoggedIn)
        {
            string FirstName = PersonalDetails.AskFirstName();
            string LastName = PersonalDetails.AskLastName();
            string Email = PersonalDetails.AskEmailAddress();
            string PhoneNumber = PersonalDetails.AskPhoneNumber();

            List<string> BookedDates = ReservationLogic.VolGeboekteDatums();
            DateTime Date = PersonalDetails.AskDate(BookedDates);
            string TimeSlot = PersonalDetails.AskTimeSlot(Date);
            int Guests = PersonalDetails.AskAmountOfGuests();
            List<string> Tables;
            Tables = ReservedTable.AssignTable(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            int GuestID = GenerateRandomGuestID();
            PreOrder preOrder = null;
            if (Messages.AskForPreOrder())
            {
                preOrder = PreOrdering.AskDish(GuestID.ToString(), Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            ReservationDataModel Reservation = new(GuestID, FirstName, LastName, PhoneNumber, Email, Date.ToString("dd-MM-yyyy"), TimeSlot, Tables, preOrder);
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
            List<string> Tables;
            Tables = ReservedTable.AssignTable(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            int GuestID = GenerateRandomGuestID();
            PreOrder preOrder = null;
            if (Messages.AskForPreOrder())
            {
                preOrder = PreOrdering.AskDish(GuestID.ToString(), Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            ReservationDataModel Reservation = new(GuestID, guest.FirstName, guest.LastName, guest.PhoneNumber, guest.EmailAddress, Date.ToString("dd-MM-yyyy"), TimeSlot, Tables, preOrder);
            ReservationLogic.AddReservationToList(Reservation);
            Messages.Thanking4Reservation(Reservation.GuestID);
        }
    }
    public static void CancelReservation(int guestID)
    {
        ReservationLogic.CancelReservation(guestID); 
    }
}
