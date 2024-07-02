using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http.Headers;

static class ManagerOptions
{

    public static void ReservationOptions()
    {
        string answer = "";
        while(answer != "1"){
        do{
        System.Console.WriteLine("[1]: Home");
        System.Console.WriteLine("[2]: See reservations on a certain date");
        System.Console.WriteLine("[3]: See reservations on a certain time and date");
        System.Console.WriteLine("[4]: Cancel reservation"); // Komt nog
        answer = Console.ReadLine().ToLower();
        if (answer == "1")
        {
            Home.Options();
            break;
        }
        } while (answer != "2" && answer != "3" && answer != "4");
        SeeReservations(answer);
        }
    }

    public static void SeeReservations(string answer)
    {
        switch (answer)
        {
            // Maak jullie geen zorgen de fouthandling van userinput ga ik later doen
            case "2":
            string date;
            do{
            System.Console.WriteLine("What date? (day-month-year)(enter 'q' to quit)");
            date = Console.ReadLine();
            if(date.ToLower().Contains("q"))
            {
                return;
            }
            string[] dateParts = date.Split("-");
            if (dateParts.Length == 3)
            {
                dateParts[0] = dateParts[0].PadLeft(2, '0');
                dateParts[1] = dateParts[1].PadLeft(2, '0'); 
                date = string.Join("-", dateParts);
            }
            } while (!CheckReservationInfo.CheckOtherDates(date));
            ReservationLogic.PrintReservationsBasedOnDate(date);
            break;
            case "3":
            string datum;
            do{
            System.Console.WriteLine("What date? (day-month-year)(enter 'q' to quit)");
            datum = Console.ReadLine();
            if(datum.ToLower().Contains("q"))
            {
                return;
            }
            string[] datumParts = datum.Split("-");
            if (datumParts.Length == 3)
            {
                datumParts[0] = datumParts[0].PadLeft(2, '0');
                datumParts[1] = datumParts[1].PadLeft(2, '0');
                datum = string.Join("-", datumParts);
            }
            } while (!CheckReservationInfo.CheckOtherDates(datum));
            System.Console.WriteLine("What time? (hours:minutes)(enter 'q' to quit)");
            string tijd = Console.ReadLine();
            if(tijd.ToLower().Contains("q"))
            {
                return;
            }
            ReservationLogic.PrintReservationBasedOnTime(datum, tijd);
            break;
            case "4":
            System.Console.WriteLine("What is the guest Id?(enter 'q' to quit)");
            string input = Console.ReadLine();
            if(input.ToLower().Contains("q"))
            {
                return;
            }
            if (int.TryParse(input, out int guestId))
            {
                ReservationLogic.CancelReservation(guestId);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid input. Please enter a valid number."); Console.ResetColor();
            }
            break;
        }
    }
    public static void ReviewOptions()
    {
        string answer = "";
        while(answer != "1"){
        do{
            //changed most of the Lettered options into numbered options
        System.Console.WriteLine("[1]: Home");
        System.Console.WriteLine("[2]: See all reviews");
        System.Console.WriteLine("[3]: See reviews based on ratings");
        System.Console.WriteLine("[4]: Reply to a review");
        System.Console.WriteLine("[5]: Delete review");
        System.Console.WriteLine("[6]: Delete all reviews");
        answer = Console.ReadLine().ToLower();
        if (answer == "1")
        {
            Home.Options();
            break;
        }
        } while (answer != "2" && answer != "3" && answer != "4" && answer != "5" && answer != "6");
        ReviewDetails(answer);
        }
    }
    public static void ReviewDetails(string answer)
    {
        switch (answer)
        {
            // Maak jullie geen zorgen de fouthandling van userinput ga ik later doen
            case "2":
            ReviewLogic.SeeAllReviews();
            break;
            case "3":
            while (true){
                Console.WriteLine("Choose rating from 1 up to 5 (Type 'Q' to quit):");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "q"){
                    break;
                }

                if (!int.TryParse(input, out int rating) || rating < 1 || rating > 5){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a whole number between 1 and 5.");
                    Console.ResetColor();  
                    continue;
                }

                ReviewLogic.SeeReviewsBasedOnRating(rating);
                break; 
            }

            break;
            break;
            case "4":
            ReviewLogic.ReplyFromManager();
            break;
            case "5":
            ReviewLogic.DeleteReview();
            break;
            case "6":
            ReviewLogic.DeleteAllReviews();
            break;
        }  
    }
public static void ChangeRestaurantInfo()
    {
        Console.Clear();
        Contact.SeeContactInfo();
        System.Console.WriteLine("[1]: Home");
        System.Console.WriteLine("[2]: Change address");
        System.Console.WriteLine("[3]: Change Email");
        System.Console.WriteLine("[4]: Change phone number");
        string answer = Console.ReadLine();
        ContactDataModel restaurantInfo = ContactAccess.ReadFromJson()[0];
        switch (answer)
        {
            case "1":
            Home.Options();
            break;
            case "2":
            System.Console.WriteLine("New address:");
            string ChangedAddress = Console.ReadLine();
            restaurantInfo.Adress = ChangedAddress;
            ContactAccess.WriteToJson(new(){new(ChangedAddress, restaurantInfo.PhoneNumber, restaurantInfo.Email)});
            ChangeRestaurantInfo();
            break;
            case "3":
            System.Console.WriteLine("New email:");
            string ChangedEmail = Console.ReadLine();
            restaurantInfo.Email = ChangedEmail;
            ContactAccess.WriteToJson(new(){new(restaurantInfo.Adress, restaurantInfo.PhoneNumber, ChangedEmail)});
            ChangeRestaurantInfo();
            break;
            case "4":
            System.Console.WriteLine("New phone number:");
            string ChangedPhoneNumber = Console.ReadLine();
            restaurantInfo.PhoneNumber = ChangedPhoneNumber;
            ContactAccess.WriteToJson(new(){new(restaurantInfo.Adress, ChangedPhoneNumber, restaurantInfo.Email)});
            ChangeRestaurantInfo();
            break;
        }
    }
}

