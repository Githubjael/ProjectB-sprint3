using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http.Headers;

static class ManagerOptions
{
    public static void EditTimeslots()
    {
        List<string> Timeslots = Home.ShowTimeSlots();
        DateTime OldTimeSlot = DateTime.ParseExact(Timeslots[0],"HH:mm", CultureInfo.GetCultureInfo("nl-NL"));
        DateTime SecondTimeSlot = DateTime.ParseExact(Timeslots[1], "HH:mm", CultureInfo.GetCultureInfo("nl-NL"));
    // Know Timeslot gap
        TimeSpan verschil = SecondTimeSlot - OldTimeSlot;
        List<string> NewTimes = new();
        System.Console.WriteLine("[H]: Home");
        System.Console.WriteLine("[1]: Edit first time slot");
        System.Console.WriteLine("[2]: Edit last time slot");
        string answer;
        do
        {
            answer = Console.ReadLine().ToLower();
            try{
            switch (answer)
            {
                case "h":
                Home.Options();
                break;
                case "1":
                Console.WriteLine("New first time slot:");
                string newTime = Console.ReadLine();
                while(DateTime.ParseExact(newTime, "HH:mm", CultureInfo.GetCultureInfo("nl-NL")) <= DateTime.ParseExact(Timeslots[Timeslots.Count - 1], "HH:mm", CultureInfo.GetCultureInfo("nl-NL")))
                {
                    NewTimes.Add(newTime);
                    newTime = DateTime.ParseExact(newTime, "HH:mm", CultureInfo.GetCultureInfo("nl-NL")).Add(verschil).ToString("HH:mm");
                }
                // Ik moet de liist eigenlijk returnen naar de logic laag en vervolgens daar writen naar json maar dat doe ik aan het eind! Done
                Home.ChangeTimeSlots(NewTimes);
                break;
                case "2":
                Console.WriteLine("New last time slot:");
                string lastTime = Console.ReadLine();
                for (int i = Timeslots.Count - 1; i >= 0; i--)
                {
                    if (DateTime.ParseExact(Timeslots[i], "HH:mm", CultureInfo.GetCultureInfo("nl-NL")) > DateTime.ParseExact(lastTime, "HH:mm", CultureInfo.GetCultureInfo("nl-NL")))
                    {
                        Timeslots.RemoveAt(i);
                    }
                }
                while (DateTime.ParseExact(lastTime, "HH:mm", CultureInfo.GetCultureInfo("nl-NL")) != DateTime.ParseExact(Timeslots[Timeslots.Count - 1], "HH:mm", CultureInfo.GetCultureInfo("nl-NL")))
                {
                    Timeslots.Add(DateTime.ParseExact(Timeslots[Timeslots.Count - 1], "HH:mm", CultureInfo.GetCultureInfo("nl-NL")).Add(verschil).ToString("HH:mm"));
                }
                Home.ChangeTimeSlots(Timeslots);
                break;  
            }
            }
             catch(Exception)
                {
                    System.Console.WriteLine("Invalid input, please fill in your answer in the desired format: HH:mm");
                }
            System.Console.WriteLine("New time slots:");
            Timeslots = Home.ShowTimeSlots();
            foreach(var Time in Timeslots)
            {
                System.Console.WriteLine(Time);
            }
            EditTimeslots();
        }while(answer != "H" && answer != "1" && answer != "2" && answer != "3"); // replace met checking method

    }

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
            case "d":
            string date;
            do{
            System.Console.WriteLine("What date? (day-month-year)");
            date = Console.ReadLine();
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
            case "t":
            string datum;
            do{
            System.Console.WriteLine("What date? (day-month-year)");
            datum = Console.ReadLine();
            string[] datumParts = datum.Split("-");
            if (datumParts.Length == 3)
            {
                datumParts[0] = datumParts[0].PadLeft(2, '0');
                datumParts[1] = datumParts[1].PadLeft(2, '0');
                datum = string.Join("-", datumParts);
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
            System.Console.WriteLine("Choose rating from 1 up to 5");
            int rating = Convert.ToInt32(Console.ReadLine());
            ReviewLogic.SeeReviewsBasedOnRating(rating);
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
        System.Console.WriteLine("[1]: Change address");
        System.Console.WriteLine("[2]: Change Email");
        System.Console.WriteLine("[3]: Change phone number");
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
        }
    }
}
