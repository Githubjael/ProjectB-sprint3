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
                        MakeReservation();
                        Options();
                        return;
                    case "CR":
                        // Cancel reservation
                        System.Console.WriteLine("Enter your guest ID"); // voorbeeld guestID deze wordt normaal ingevoerd in program maar dat zien we later wel
                        int guestID = Convert.ToInt32(Console.ReadLine());
                        ReservationLogic.CancelReservation(guestID);
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

    public static void MakeReservation()
    {
        if(Home.IsLoggedIn)
        {
            System.Console.WriteLine($"Hi {Home.guestName}! please provide us with the following details:");
        }
        else{
            System.Console.WriteLine("We are honored by your interest in booking at our restaurant!");
            System.Console.WriteLine("Please answer the following questions:");
        }
        if(!Home.IsLoggedIn)
        {
            string FirstName = AskFirstName();
            string LastName = AskLastName();
            string Email = AskEmailAddress();
            string PhoneNumber = AskPhoneNumber();
            DateTime Date = AskDate();
            string TimeSlot = AskTimeSlot(Date);
            int Guests = AskAmountOfGuests();
              List<Table> Tables;
            if (Guests > 6)
            {
                Tables =  ReservedTable.AssignTables(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            else{
                Tables = ReservedTable.AssignTable(Guests, Date.ToString("dd-MM-yyyy"), TimeSlot);
            }
            int GuestID = GenerateRandomGuestID();
            ReservationDataModel Reservation = new(GuestID, FirstName, LastName, PhoneNumber, Email, Date.ToString("dd-MM-yyyy"), TimeSlot, Tables);
            ReservationLogic.AddReservationToList(Reservation);
            if (Tables.Count == 1)
            {
            System.Console.WriteLine($"Thanks for booking with us! Your guest ID = {GuestID} and your Table Id = {Reservation.Tables[0].ID}");
            }
            else
            {
            System.Console.WriteLine($"Thanks for booking with us! Your guest ID = {GuestID} and your Table are:");
            foreach(Table table in Reservation.Tables)
            {
                System.Console.WriteLine(table.ID);
            }

            }
        }
        else
        {
            Guest guest = UsersAccess.GetUser(Home.guestEmail);
            DateTime Date = AskDate();
            int Guests = AskAmountOfGuests();
            string TimeSlot = AskTimeSlot(Date);
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
            if (Tables.Count == 1)
            {
            System.Console.WriteLine($"Thanks for booking with us! Your guest ID = {GuestID} and your Table Id = {Reservation.Tables[0].ID}");
            }
            else
            {
            System.Console.WriteLine($"Thanks for booking with us! Your guest ID = {GuestID} and your Table are:");
            foreach(Table table in Reservation.Tables)
            {
                System.Console.WriteLine(table.ID);
            }

            }
        }
    }

    public static string AskFirstName()
    {
        string FirstName;
        do{
        System.Console.WriteLine("What is your first name?");
        FirstName = Console.ReadLine();
        } while (!CheckReservationInfo.CheckFirstName(FirstName));
        return FirstName;
    }

    public static string AskLastName()
    {
        string LastName;
        do{
        System.Console.WriteLine("What is your last name?");
        LastName = Console.ReadLine();
        } while (!CheckReservationInfo.CheckLastName(LastName));
        return LastName;
    }

    public static string AskPhoneNumber()
    {
        string PhoneNumber;
        do{
        System.Console.WriteLine("What is your phone number?");
        PhoneNumber = Console.ReadLine();
        } while (!CheckReservationInfo.CheckPhoneNumber(PhoneNumber));
        return  PhoneNumber;
    }

    public static string AskEmailAddress()
    {
        string EmailAddress;
        do{
        System.Console.WriteLine("What is your email address?");
        EmailAddress = Console.ReadLine();
        } while (!CheckReservationInfo.CheckEmail(EmailAddress));
        return EmailAddress;
    }

    public static DateTime AskDate()
    {
    {
        string date;
        do
        {
            Console.WriteLine("When do you want to book? Provide the date in the following format:\nday-month-year.");
            date = Console.ReadLine();

            // Ensure day and month have leading zeros if needed
            string[] dateParts = date.Split("-");
            if (dateParts.Length == 3)
            {
                dateParts[0] = dateParts[0].PadLeft(2, '0'); // Add leading zero to day
                dateParts[1] = dateParts[1].PadLeft(2, '0'); // Add leading zero to month
                date = string.Join("-", dateParts); // Reconstruct the date string
            }

        } while (!CheckReservationInfo.CheckDate(date));

        // Parse the validated date string into a DateTime object
        DateTime reservationDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
        return reservationDate;
    }
    }

    public static string AskTimeSlot(DateTime Date)
    {
        string answer;
        List<string> TimeSlots = ReservedTable.GetTimes(Date.ToString("dd-MM-yyyy"));
        do {
        System.Console.WriteLine("Available TimeSlots:");
        System.Console.WriteLine("Type the number of desired timeslot please.");
        System.Console.WriteLine();
        int Number = 1;
        for (int i = 0; i < TimeSlots.Count; i++)
        {
            System.Console.WriteLine($"[{Number}]: {TimeSlots[i]}");
            Number++;
        }
        answer = Console.ReadLine();
        } while (!CheckReservationInfo.CheckTimeSlot(answer, TimeSlots));
        return TimeSlots[Convert.ToInt32(answer) - 1];
    }

    public static int AskAmountOfGuests()
    {
        string Guests;
        do
        {
            System.Console.WriteLine("Our restaurant serves a maximum of 52 guests at a timeslot");
            // We hebben op het gekozen tijdstip nog bla bla tafels over en dus plek voor bla bla gasten
            // Dat ga ik wat later doen btw
            System.Console.WriteLine("Right now we have x tables available and therefore place for y guests at {chosen timeslot}.");
            // Vraag user of hij datum of tijd wilt veranderen...
            System.Console.WriteLine("How many guests are coming including yourself?");
            Guests = Console.ReadLine();
        } while(!CheckReservationInfo.CheckGuests(Guests));
        return Convert.ToInt32(Guests);
    }
    public static void CancelReservation(int guestID)
    {
        ReservationLogic.CancelReservation(guestID); 
    }
}
