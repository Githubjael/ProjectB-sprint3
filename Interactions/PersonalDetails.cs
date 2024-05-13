static class PersonalDetails
{
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

    public static DateTime AskDate(List<string> BookedDates)
    {
    {
        string date;
        List<string> BookedDatesNotFull = ReservationLogic.GeboekteDatums();
        System.Console.WriteLine();
        if (!(BookedDatesNotFull.Count == 0 && BookedDates.Count == 0)){
        System.Console.WriteLine("Please note that availability is limited on the following dates; apologies for any inconvenience.");
        }
        foreach(string BookedDate in BookedDates)
        {
            System.Console.WriteLine($"- {BookedDate} (fully booked)");
        }
        foreach(string BookedDate in BookedDatesNotFull)
        {
            System.Console.WriteLine($"- {BookedDate}");
        }
        do
        {
            System.Console.WriteLine();
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

        } while (!CheckReservationInfo.CheckDate(date, BookedDates));

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
        int GuestsInt;
        do
        {
            System.Console.WriteLine("Our restaurant serves a maximum of 52 guests at a timeslot");
                // We hebben op het gekozen tijdstip nog bla bla tafels over en dus plek voor bla bla gasten
                // Dat ga ik wat later doen btw
                // Vraag user of hij datum of tijd wilt veranderen...
            System.Console.WriteLine("How many guests are coming including yourself?");
            Guests = Console.ReadLine();
            if (!int.TryParse(Guests, out GuestsInt))
            {
                System.Console.WriteLine("Please enter a valid number.");
                continue; // Prompt user again
            }

            if (GuestsInt > 52)
            {
                System.Console.WriteLine("Sorry, our restaurant can accommodate a maximum of 52 guests at a timeslot.");
                continue; // Prompt user again
            }

        } while (!CheckReservationInfo.CheckGuests(Guests)); // Loop until the input is valid

        return GuestsInt;
    }
}
