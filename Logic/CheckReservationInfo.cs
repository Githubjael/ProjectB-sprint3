using System.Text.RegularExpressions;
class CheckReservationInfo
{
    // Check of alle characters in voornaam letter zijn
    public static bool CheckFirstName(string FirstName)
    {
        if (string.IsNullOrEmpty(FirstName))
        {
            Console.WriteLine("*You must fill something in.");
            return false;
        }
        foreach(char letter in FirstName)
        {
            if (!Char.IsLetter(letter))
            {
                Console.WriteLine("*Your first name must only contain letters.");
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
            Console.WriteLine("*You must fill something in.");
            return false;
        }
        foreach(char letter in LastName)
        {
            if (!Char.IsLetter(letter))
            {
                if (!Char.IsWhiteSpace(letter)){
                Console.WriteLine("*Your last name must only contain letters.");
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
            System.Console.WriteLine("*You must fill something in.");
            return false;
        }
        foreach(char number in PhoneNumber)
        {
            if (!Char.IsNumber(number))
            {
                System.Console.WriteLine("*Your phone number must only contain numbers.");
                return false;
            }
        }
        if (PhoneNumber.Length != 10)
        {
            System.Console.WriteLine("*Your phone number must contain 10 numbers.");
            return false;
        }
        return true;
    }

    // check of emailadres een '@' en '.' bevat
    // ook nog met yahoo, gmail, hotmail etc etc dat moet ook nog containen
    public static bool CheckEmail(string EmailAddress)
    {
        if (string.IsNullOrEmpty(EmailAddress))
        {
            System.Console.WriteLine("*Please fill something in.");
            return false;
        }
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|hr|nl|be|en)$";
        if (Regex.IsMatch(EmailAddress, regex, RegexOptions.IgnoreCase))
        {
            return true;
        }

        else
        {
            System.Console.WriteLine("*Your email address is invalid.");
            return false;
        }
    }


public static bool CheckDate(string date, List<string> FullyBookedDates)
    {
        if (FullyBookedDates.Contains(date))
        {
            System.Console.WriteLine("The date you're interested in is fully booked.");
            return false;
        }
        if (string.IsNullOrEmpty(date))
        {
            System.Console.WriteLine("*Please fill something in.");
            return false;
        }
        foreach (char c in date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.WriteLine("Invalid characters detected. Please provide the date in the format: dd-MM-yyyy.");
                return false;
            }
        }

        // Attempt to parse the date string into a DateTime object
        if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
        if (string.IsNullOrEmpty(date))
        {
            System.Console.WriteLine("*Please fill something in.");
            return false;
        }
        foreach (char c in date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.WriteLine("Invalid characters detected. Please provide the date in the format: dd-MM-yyyy.");
                return false;
            }
        }
            // Check if the parsed date is in the past
            if (parsedDate.Date < DateTime.Today || ReservedTable.GetTimes(date).Count == 0)
            {
                Console.WriteLine("Please provide a future date.");
                return false;
            }
            // Date is valid
            return true;
        }
        else
        {
            Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy.");
            return false;
        }
    }

    public static bool CheckOtherDates(string Date)
    {
        if (string.IsNullOrEmpty(Date))
        {
            System.Console.WriteLine("*Please fill something in.");
            return false;
        }
        foreach (char c in Date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.WriteLine("Invalid characters detected. Please provide the Date in the format: dd-MM-yyyy.");
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
                Console.WriteLine("Invalid characters detected. Please provide the Date in the format: dd-MM-yyyy.");
                return false;
            }
        }
            // Date is valid
            return true;
        }
        else
        {
            Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy.");
            return false;
        }
    }


    public static bool CheckTimeSlot(string answer, List<string> TimeSlots)
    {
        if (string.IsNullOrEmpty(answer))
        {
            System.Console.WriteLine("*Please fill a number in.");
            return false;
        }
        try
        {
            Convert.ToInt32(answer);
        }
        catch (Exception)
        {
            System.Console.WriteLine($"*'{answer}' is an invalid number. Please fill a valid number in");
            return false;
        }
        if (Convert.ToInt32(answer) > TimeSlots.Count)
        {
            System.Console.WriteLine($"*Number should be below {TimeSlots.Count}");
            return false;
        }
        else if (Convert.ToInt32(answer) < 0)
        {
            System.Console.WriteLine("*Only enter positive numbers please.");
            return false;
        }
        return true;
    }

    public static bool CheckGuests(string Guests) // Later komt nog nakijken op max amount of Guests yk
    {
        if (string.IsNullOrEmpty(Guests))
        {
            System.Console.WriteLine("You must fill something in.");
            return false;
        }
        try
        {
            Convert.ToInt32(Guests);
        }
        catch (Exception)
        {
            System.Console.WriteLine($"'{Guests}' is an invalid number.");
            return false;
        }
        return true;
    }
}
