static class PersonalDetails
{
    public static string AskFirstName()
    {
        string FirstName;
        do{
        System.Console.WriteLine("What is your first name? (Name has to be between 2-20 characters)");
        FirstName = Console.ReadLine();
        if (FirstName == "q" || FirstName == "Q")
        {
            
            Console.WriteLine("Reservation stopped");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(". . . . .");
            System.Threading.Thread.Sleep(1000);
            Home.Options(); 
            return "";
        }
    } while (!CheckReservationInfo.CheckFirstName(FirstName));

        return FirstName;
    }

    public static string AskLastName()
    {
        string LastName;
        do{
        System.Console.WriteLine("What is your last name?  (Name has to be between 2-20 characters)");
        LastName = Console.ReadLine();
        if (LastName == "q" || LastName == "Q")
        {
            Console.WriteLine("Reservation stopped");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(". . . . .");
            System.Threading.Thread.Sleep(1000);
            Home.Options(); 
            return "";
        }
        } while (!CheckReservationInfo.CheckLastName(LastName));
        return LastName;
    }

    public static string AskPhoneNumber()
    {
        string PhoneNumber;
        do{
        System.Console.WriteLine("What is your phone number?");
        PhoneNumber = Console.ReadLine();
        if (PhoneNumber == "q" || PhoneNumber == "Q")
        {
            Console.WriteLine("Reservation stopped");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(". . . . .");
            System.Threading.Thread.Sleep(1000);
            Home.Options(); 
            return "";
        }
        } while (!CheckReservationInfo.CheckPhoneNumber(PhoneNumber));
        return PhoneNumber;

    }

    public static string AskEmailAddress()
    {
        string EmailAddress;
        do{
        System.Console.WriteLine("What is your email address?");
        EmailAddress = Console.ReadLine();
        if (EmailAddress == "q" || EmailAddress == "Q")
        {
            Console.WriteLine("Reservation stopped");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(". . . . .");
            System.Threading.Thread.Sleep(1000);
            Home.Options(); 
            return "";
        }
        } while (!CheckReservationInfo.CheckEmail(EmailAddress));
        return EmailAddress;
    }

    public static DateTime AskDate(List<string> BookedDates)
    {
    {
        string date;
        List<string> BookedDatesNotFull = ReservationLogic.GeboekteDatums();
        System.Console.WriteLine();
        if (!(BookedDatesNotFull.Count == 0 || BookedDates.Count == 0)){
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
            if (date == "q" || date == "Q")
            {
                Console.WriteLine("Reservation stopped");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine(". . . . .");
                System.Threading.Thread.Sleep(1000);
                Home.Options(); 
                return default;
            }
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
        if (answer == "q" || answer == "Q")
        {
            Console.WriteLine("Reservation stopped");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(". . . . .");
            System.Threading.Thread.Sleep(1000);
            Home.Options(); 
            return "";
        }
        } while (!CheckReservationInfo.CheckTimeSlot(answer, TimeSlots));
        return TimeSlots[Convert.ToInt32(answer) - 1];
    }

        public static int AskAmountOfGuests()
        {
            string guestsInput;
            int guests;

            do
            {
            Console.WriteLine("How many guests are coming including yourself?");
            guestsInput = Console.ReadLine();   
            if (guestsInput == "q" || guestsInput == "Q")
            {
                Console.WriteLine("Reservation stopped");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine(". . . . .");
                System.Threading.Thread.Sleep(1000);
                Home.Options();
                return 0;
            }

                if (int.TryParse(guestsInput, out guests)){
                    if (guests > 6){
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("If you want to make a reservation for more than 6 people, please call the restaurant."); Console.ResetColor();
                    }
                    else if (guests <= 0){
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Please enter a positive number."); Console.ResetColor();
                    }
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Please enter a valid number."); Console.ResetColor();
                }
            } while (!int.TryParse(guestsInput, out guests) || guests <= 0 || guests > 6 || !CheckReservationInfo.CheckGuests(guestsInput));

            return guests;
        }
}
