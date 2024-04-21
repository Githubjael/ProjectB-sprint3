class Reservation : Page
{
    public static List<int> unavailableGuestIDs = new List<int>(); // ik een lijst om de gebruikte Guests IDs op te slaan
    private static Random random = new Random();
    public static int GenerateRandomGuestID()
    {
        int guestID;
        do
        {
            guestID = random.Next(1, 17);
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
        ReservedTable.PopulateTables(); // Maakt Tafelobjecten aan
        // Vraag om Voornaam
        string FirstName;
        do{
        System.Console.WriteLine("What is your first name?");
        FirstName = Console.ReadLine();
        } while (!CheckReservationInfo.CheckFirstName(FirstName));


        // Vraag achternaam
        string LastName;
        do{
        System.Console.WriteLine("What is your last name?");
        LastName = Console.ReadLine();
        } while(!CheckReservationInfo.CheckLastName(LastName));

        // Vraag telefoonnummer
        string PhoneNumber;
        do{
        System.Console.WriteLine("What is your phone number?");
        PhoneNumber = Console.ReadLine();
        } while (!CheckReservationInfo.CheckPhoneNumber(PhoneNumber));

        string EmailAddress;
        // Vraag emailadres
        do{
        System.Console.WriteLine("What is your email address?");
        EmailAddress = Console.ReadLine();
        } while (!CheckReservationInfo.CheckEmailAddress(EmailAddress));
        // vraag welke jaar gast wilt boeken
        string ChosenYearString;
        do
        {
            System.Console.WriteLine("In what year would you like to book?");
            ChosenYearString = Console.ReadLine();
        } while(!CheckReservationInfo.CheckChosenYear(ChosenYearString));
        int ChosenYear = Convert.ToInt32(ChosenYearString);
        // Vraag in welke maand de gast wilt komen
        string ChosenMonthString;
        do{
        System.Console.WriteLine("What month would you like to book? Enter number of month.");
        ChosenMonthString = Console.ReadLine();
        } while (!CheckReservationInfo.CheckChosenMonth(ChosenMonthString, ChosenYear));
        int ChosenMonth = Convert.ToInt32(ChosenMonthString);


        // Welke Dag
        // vraag de gebruiker om een dag te kiezen
        string ChosenDayString;
        do{
        Console.WriteLine($"Available days for booking are:\n{string.Join(", ", DisplayMonthList.GiveListBasedOnMonth(ChosenMonth, ChosenYear))}.\nChoose a day.");
        ChosenDayString = Console.ReadLine();
        } while (!CheckReservationInfo.CheckChosenDay(ChosenDayString, ChosenMonth, ChosenYear));
        int ChosenDay = Convert.ToInt32(ChosenDayString);


        // Vraag hoeveel Personen komen
        string Guests;
        do{
        System.Console.WriteLine("How many guests are coming including yourself?");
        Guests = Console.ReadLine();
        } while (!CheckReservationInfo.CheckGuests(Guests));
        int guests = Convert.ToInt32(Guests);

        // HIER KOMEN DE TIJDSTIPPEN TE STAAN
        // voor tijdstippen moet ik checken of er wel tafels beschikbaar zijn
        string ChosenTime;
        do{
        Console.WriteLine($"Available time slots for booking are:\n{string.Join(", ", DisplayDayList.GiveListBasedOnDay(ChosenDay, ChosenMonth, ChosenYear))}.\nChoose a time slot.");
            System.Console.WriteLine("");
            ChosenTime = Console.ReadLine();
        } while (!CheckReservationInfo.CheckTime(ChosenTime));
        string ChosenMonthFinal;
        string ChosenDayFinal;
        if (ChosenMonth < 10 && ChosenDay < 10)
        {
            ChosenMonthFinal = $"0{ChosenMonth}";
            ChosenDayFinal = $"0{ChosenDay}";
        }
        else if (ChosenMonth < 10)
        {
            ChosenMonthFinal = $"0{ChosenMonth}";
            ChosenDayFinal =Convert.ToString(ChosenDay);
        }
        else if (ChosenDay < 10)
        {
            ChosenDayFinal = $"0{ChosenDay}";
            ChosenMonthFinal = Convert.ToString(ChosenMonth);
        }
        else{
            ChosenMonthFinal = Convert.ToString(ChosenMonth);
            ChosenDayFinal =Convert.ToString(ChosenDay);
        }
        // toon de reserveringsinformatie
        Console.WriteLine($"Your reservation details:\n{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear} at {ChosenTime}, for {guests} guests");

        string confirmation;
        bool valid = false; 
        int guestID = GenerateRandomGuestID();
        do
        {
            Console.WriteLine("Do you confirm your reservation? y/n");
            confirmation = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(confirmation))
            {
                Console.WriteLine("Invalid input. You must enter 'y' (yes) or 'n' (no)");
            }
            else if (!confirmation.All(char.IsLetter))
            {
                Console.WriteLine("Invalid input. You must enter 'y' (yes) or 'n' (no)");
            }
            else
            {
                valid = true; 
            }
        } while (!valid);

        if (confirmation == "y")
        {
            if (guests > 6){
                List<Table> ChosenTables = ReservedTable.AssignTable(guests);
                // We maken een object van de Reservering om in een lijst te dumpen om naar json te sturen
                // We maken een object van de Reservering om in een lijst te dumpen om naar json te sturen
                List<Table> Tables = new(){};
                foreach(Table table in ChosenTables){
                    Tables.Add(table);
                }
                ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, Tables);
                ReservationLogic.AddReservationToList(Reservation);
                // bevestig de reservering aan de gebruiker
                Console.WriteLine($"Your reservation is confirmed.\nThank you for choosing our restaurant, we look forward to serving you!");
                string tableids = "";
                foreach (Table table in ChosenTables)
                {
                    tableids += $"{table.ID} ";
                }
                Console.WriteLine($"Your Guest ID {guestID}, Your table numbers = {tableids}");
            }
            else
            {
                var tabletype = guests switch
                {
                    1 => "2",
                    2 => "2",
                    3 => "4",
                    4 => "4",
                    5 => "6",
                    6 => "6",
                    _ => "?"
                };

                var found = ReservedTable.TableTracker.Find(x => x.Type == Convert.ToInt32(tabletype) && x.Reserved == false); // Waarom null? // er staat does Type (0) equals 2 personstable? 
                // found.GuestID = guestID;
                if (found is null){
                    ReservationLogic.SwitchIfNull(found, Convert.ToInt16(tabletype),guestID, FirstName, LastName, PhoneNumber, EmailAddress, ChosenDayFinal, ChosenMonthFinal, ChosenYear, ChosenTime);
                }
                else{
                    found.IsReserved();
                }
                List<Table> table = new(){found};
                // We maken een object van de Reservering om in een lijst te dumpen om naar json te sturen
                ReservationDataModel Reservation = new ReservationDataModel(guestID, FirstName, LastName, PhoneNumber, EmailAddress, $"{ChosenDayFinal}/{ChosenMonthFinal}/{ChosenYear}", ChosenTime, table);
                ReservationLogic.AddReservationToList(Reservation);
                // bevestig de reservering aan de gebruiker
                Console.WriteLine($"Your reservation is confirmed.\nThank you for choosing our restaurant, we look forward to serving you!");
                Console.WriteLine($"Your Guest ID {Reservation.GuestID}, Your table number = {Reservation.Tables[0].ID}");

            }
        }
        else if (confirmation == "n")
        {
            Console.WriteLine("Your reservation is not confirmed, Bye!");
        }
}

    public static void CancelReservation(int guestID)
    {
        ReservationLogic.CancelReservation(guestID); 
    }
}
 
