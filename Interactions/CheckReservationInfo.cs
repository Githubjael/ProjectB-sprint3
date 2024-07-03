using System.Text.RegularExpressions;


public static class CheckReservationInfo
{
    public static bool CheckFirstName(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }
        if (firstName.Length < 2 || firstName.Length > 20)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Your first name needs to be 2-20 characters.");
            Console.ResetColor();
            return false;
        }
        foreach (char letter in firstName)
        {
            if (!char.IsLetter(letter) && !char.IsWhiteSpace(letter))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Your first name must only contain letters.");
                Console.ResetColor();
                return false;
            }
        }
        return true;
    }

    public static bool CheckLastName(string lastName)
    {
        if (string.IsNullOrEmpty(lastName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }
        if (lastName.Length < 2 || lastName.Length > 20)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Your last name needs to be 2-20 characters.");
            Console.ResetColor();
            return false;
        }
        foreach (char letter in lastName)
        {
            if (!char.IsLetter(letter) && !char.IsWhiteSpace(letter))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Your last name must only contain letters.");
                Console.ResetColor();
                return false;
            }
        }
        return true;
    }

    public static bool CheckPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }
        foreach (char number in phoneNumber)
        {
            if (!char.IsNumber(number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*Your phone number must only contain numbers.");
                Console.ResetColor();
                return false;
            }
        }
        if (phoneNumber.Length != 10)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Your phone number must contain 10 numbers.");
            Console.ResetColor();
            return false;
        }
        return true;
    }

    public static bool CheckEmail(string emailAddress)
    {
        if (string.IsNullOrEmpty(emailAddress))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }

        string regex = @"^[^@\s]+@(?:yahoo|gmail|hotmail|outlook|live|aol|icloud|hr)\.(com|net|org|gov|hr|nl|be|en)$";

        if (Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase))
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

    public static bool CheckDate(string date, List<string> fullyBookedDates)
    {
        if (fullyBookedDates.Contains(date))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The date you're interested in is fully booked.");
            Console.ResetColor();
            return false;
        }
        if (string.IsNullOrEmpty(date))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }
        foreach (char c in date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid characters detected. Please provide the date in the format: dd-MM-yyyy.");
                Console.ResetColor();
                return false;
            }
        }

        if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            if (parsedDate.Date < DateTime.Today || ReservedTable.GetTimes(date).Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please provide a future date.");
                Console.ResetColor();
                return false;
            }
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy.");
            Console.ResetColor();
            return false;
        }
    }

    public static bool CheckOtherDates(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }
        foreach (char c in date)
        {
            if (!char.IsDigit(c) && c != '-')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy.");
                Console.ResetColor();
                return false;
            }
        }

        if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            return true;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid date format. Please provide the date in the format: dd-MM-yyyy.");
            Console.ResetColor();
            return false;
        }
    }

    public static bool CheckTimeSlot(string answer, List<string> timeSlots)
    {
        if (string.IsNullOrEmpty(answer))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill a number in.");
            Console.ResetColor();
            return false;
        }
        if (!int.TryParse(answer, out int slotNumber) || slotNumber <= 0 || slotNumber > timeSlots.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"*Number should be between 1 and {timeSlots.Count}.");
            Console.ResetColor();
            return false;
        }
        return true;
    }

    public static bool CheckGuests(string guests)
    {
        if (string.IsNullOrEmpty(guests))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*Please fill something in.");
            Console.ResetColor();
            return false;
        }
        if (!int.TryParse(guests, out _))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"'{guests}' is an invalid number.");
            Console.ResetColor();
            return false;
        }
        return true;
    }
}
