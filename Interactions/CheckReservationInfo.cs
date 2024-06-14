using System.Text.RegularExpressions;
class CheckReservationInfo
{
    // Check of alle characters in voornaam letter zijn
    public static bool CheckFirstName(string FirstName)
    {
        if (string.IsNullOrEmpty(FirstName))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        if (FirstName.Length < 2 || FirstName.Length > 20){
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Your first name needs to be 2-20 characters."); Console.ResetColor();
            return false;
        }
        foreach(char letter in FirstName)
        {
            if (!Char.IsLetter(letter))
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Your first name must only contain letters."); Console.ResetColor();
                return false;
            }
        }
        return true;
    }

    // check of alle characters in achternaam letters zijn
    public static bool CheckLastName(string LastName)
    {
        if (string.IsNullOrEmpty(LastName))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        if (LastName.Length < 2 || LastName.Length > 20){
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Your last name needs to be 2-20 characters."); Console.ResetColor();
            return false;
        }
        foreach(char letter in LastName)
        {
            if (!Char.IsLetter(letter))
            {
                if (!Char.IsWhiteSpace(letter)){
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Your last name must only contain letters."); Console.ResetColor();
                return false;
                }
            }
        }
        return true;
    }

    // check of alle characters in telefoonnummer nummers zijn
    // telefoon nummer moet niet kleiner of groter zijn dan 10
    public static bool CheckPhoneNumber(string PhoneNumber)
    {
        if (string.IsNullOrEmpty(PhoneNumber))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        foreach(char number in PhoneNumber)
        {
            if (!Char.IsNumber(number))
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Your phone number must only contain numbers."); Console.ResetColor();
                return false;
            }
        }
        if (PhoneNumber.Length != 10)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Your phone number must contain 10 numbers."); Console.ResetColor();
            return false;
        }
        return true;
    }


    public static bool CheckEmail(string EmailAddress)
    {
        if (string.IsNullOrEmpty(EmailAddress))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }

        
        string regex = @"^[^@\s]+@(?:yahoo|gmail|hotmail|outlook|live|aol|icloud)\.(com|net|org|gov|hr|nl|be|en)$";
        
        if (Regex.IsMatch(EmailAddress, regex, RegexOptions.IgnoreCase))
        {
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Your email address is invalid.");
            Console.ResetColor();
            return false;
        }
    }


public static bool CheckDate(string date, List<string> FullyBookedDates)
    {
        if (FullyBookedDates.Contains(date))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("The date you're interested in is fully booked."); Console.ResetColor();
            return false;
        }
        if (string.IsNullOrEmpty(date))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        foreach (char c in date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid characters detected. Please provide the date in the format: dd-MM-yyyy."); Console.ResetColor();
                return false;
            }
        }

        // Attempt to parse the date string into a DateTime object
        if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
        if (string.IsNullOrEmpty(date))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        foreach (char c in date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid characters detected. Please provide the date in the format: dd-MM-yyyy."); Console.ResetColor();
                return false;
            }
        }
            // Check if the parsed date is in the past
            if (parsedDate.Date < DateTime.Today || ReservedTable.GetTimes(date).Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Please provide a future date."); Console.ResetColor();
                return false;
            }
            // Date is valid
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy."); Console.ResetColor();
            return false;
        }
    }

    public static bool CheckOtherDates(string Date)
    {
        if (string.IsNullOrEmpty(Date))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        foreach (char c in Date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy."); Console.ResetColor();
                return false;
            }
        }

        // Attempt to parse the Date string into a DateTime object
        if (DateTime.TryParseExact(Date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
        foreach (char c in Date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy."); Console.ResetColor();
                return false;
            }
        }
            // Date is valid
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy."); Console.ResetColor();
            return false;
        }
    }


    public static bool CheckTimeSlot(string answer, List<string> TimeSlots)
    {
        if (string.IsNullOrEmpty(answer))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill a number in."); Console.ResetColor();
            return false;
        }
        try
        {
            Convert.ToInt32(answer);
        }
        catch (Exception)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"*'{answer}' is an invalid number. Please fill a valid number in"); Console.ResetColor();
            return false;
        }
        if (Convert.ToInt32(answer) > TimeSlots.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"*Number should be below {TimeSlots.Count}"); Console.ResetColor();
            return false;
        }
        else if (Convert.ToInt32(answer) < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Only enter positive numbers please."); Console.ResetColor();
            return false;
        }
        return true;
    }

    public static bool CheckGuests(string Guests) // Later komt nog nakijken op max amount of Guests yk
    {
        if (string.IsNullOrEmpty(Guests))
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("*Please fill something in."); Console.ResetColor();
            return false;
        }
        try
        {
            Convert.ToInt32(Guests);
        }
        catch (Exception)
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"'{Guests}' is an invalid number."); Console.ResetColor();
            return false;
        }
        return true;
    }
}
