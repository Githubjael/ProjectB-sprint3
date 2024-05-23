static class ManagerOptions
{
    public static void ReservationOptions()
    {
        string answer = "";
        while(answer != "h"){
        do{
        System.Console.WriteLine("[H]: Home");
        System.Console.WriteLine("[S]: See all reservations");
        System.Console.WriteLine("[D]: See reservations on a certain date");
        System.Console.WriteLine("[T]: See reservations on a certain time and date");
        System.Console.WriteLine("[C]: Cancel reservation"); // Komt nog
        answer = Console.ReadLine().ToLower();
        if (answer == "h")
        {
            Home.Options();
            break;
        }
        } while (answer != "s" && answer != "d" && answer != "t" && answer != "c");
        SeeReservations(answer);
        }
    }

    public static void SeeReservations(string answer)
    {
        switch (answer)
        {
            // Maak jullie geen zorgen de fouthandling van userinput ga ik later doen
            case "s":
            ReservationLogic.PrintAllReservations();
            break;
            case "d":
            string date;
            do{
            System.Console.WriteLine("What date? (day-month-year)");
            date = Console.ReadLine();
            string[] dateParts = date.Split("-");
            if (dateParts.Length == 3)
            {
                dateParts[0] = dateParts[0].PadLeft(2, '0'); // Add leading zero to day
                dateParts[1] = dateParts[1].PadLeft(2, '0'); // Add leading zero to month
                date = string.Join("-", dateParts); // Reconstruct the date string
            }
            } while (!CheckReservationInfo.CheckOtherDates(date));
            ReservationLogic.PrintReservationsBasedOnDate(date);
            break;
            case "t":
            string datum;
            do{
            System.Console.WriteLine("What date? (day-month-year)");
            datum = Console.ReadLine();
            string[] datumParts = datum.Split("-");
            if (datumParts.Length == 3)
            {
                datumParts[0] = datumParts[0].PadLeft(2, '0'); // Add leading zero to day
                datumParts[1] = datumParts[1].PadLeft(2, '0'); // Add leading zero to month
                datum = string.Join("-", datumParts); // Reconstruct the date string
            }
            } while (!CheckReservationInfo.CheckOtherDates(datum));
            System.Console.WriteLine("What time? (hours:minutes)");
            string tijd = Console.ReadLine();
            ReservationLogic.PrintReservationBasedOnTime(datum, tijd);
            break;
            case "c":
            System.Console.WriteLine("What is the guest Id?");
            int GuestId = Convert.ToInt32(Console.ReadLine());
            ReservationLogic.CancelReservation(GuestId);
            break;
        }
    }
    public static void ReviewOptions()
    {
        string answer = "";
        while(answer != "h"){
        do{
        System.Console.WriteLine("[H]: Home");
        System.Console.WriteLine("[S]: See all reviews");
        System.Console.WriteLine("[SB]: See reviews based on ratings");
        System.Console.WriteLine("[D] Delete review");
        System.Console.WriteLine("[DA] Delete all reviews");
        answer = Console.ReadLine().ToLower();
        if (answer == "h")
        {
            Home.Options();
            break;
        }
        } while (answer != "s" && answer != "sb" && answer != "d" && answer != "da");
        ReviewDetails(answer);
        }
    }
    public static void ReviewDetails(string answer)
    {
        switch (answer)
        {
            // Maak jullie geen zorgen de fouthandling van userinput ga ik later doen
            case "s":
            ReviewLogic.SeeAllReviews();
            break;
            case "sb":
            System.Console.WriteLine("Choose rating from 1 up to 5");
            int rating = Convert.ToInt32(Console.ReadLine());
            ReviewLogic.SeeReviewsBasedOnRating(rating);
            break;
            case "d":
            ReviewLogic.DeleteReview();
            break;
            case "da":
            ReviewLogic.DeleteAllReviews();
            break;
        }  
    }
    public static void ChangeRestaurantInfo()
    {
        System.Console.WriteLine("[1]: Change address");
        System.Console.WriteLine("[2]: Change Email");
        System.Console.WriteLine("[3]: Change phone number");
        System.Console.WriteLine("[4]: Change all of the above");
        string answer = Console.ReadLine();
        ContactDataModel restaurantInfo = ContactAccess.ReadFromJson()[0];
        switch (answer)
        {
            case "1":
            System.Console.WriteLine("New address:");
            string ChangedAddress = Console.ReadLine();
            restaurantInfo.Adress = ChangedAddress;
            ContactAccess.WriteToJson(new(){new(ChangedAddress, restaurantInfo.PhoneNumber, restaurantInfo.Email)});
            break;
            case "2":
            System.Console.WriteLine("New email:");
            string ChangedEmail = Console.ReadLine();
            restaurantInfo.Email = ChangedEmail;
            ContactAccess.WriteToJson(new(){new(restaurantInfo.Adress, restaurantInfo.PhoneNumber, ChangedEmail)});
            break;
            case "3":
            System.Console.WriteLine("New phone number:");
            string ChangedPhoneNumber = Console.ReadLine();
            restaurantInfo.PhoneNumber = ChangedPhoneNumber;
            ContactAccess.WriteToJson(new(){new(restaurantInfo.Adress, ChangedPhoneNumber, restaurantInfo.Email)});
            break;
            case "4":
            System.Console.WriteLine("New address:");
            ChangedAddress = Console.ReadLine();
            restaurantInfo.Adress = ChangedAddress;

            System.Console.WriteLine("New email:");
            ChangedEmail = Console.ReadLine();
            restaurantInfo.Email = ChangedEmail;

            System.Console.WriteLine("New phone number:");
            ChangedPhoneNumber = Console.ReadLine();
            restaurantInfo.PhoneNumber = ChangedPhoneNumber;
            ContactAccess.WriteToJson(new(){new(ChangedAddress, ChangedPhoneNumber, ChangedEmail)});
            break;
        }
    }
}
