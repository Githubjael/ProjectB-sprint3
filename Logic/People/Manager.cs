using System.Formats.Asn1;

class Manager : Person, IEmployee
{
    // Wat is uniek aan een manager?
    // Managers hebben een Employee Code!
    // Employee Code kan alleen maar gelezen worden en wordt zelf binnen de class gemaakt?
    private string _employeeCode = "AppelTaart";
    public string EmployeeCode => _employeeCode;
    public Manager(string firstName, string lastName, string emailAddress, string phoneNumber, string password) : base(firstName, lastName, emailAddress, phoneNumber, password)
    {
    }
// [1] Change the restaurant info
// [2] Change the menu
// [3] See all reservations

    public void ChangeRestaurantInfo(string adres, string email, string phoneNumber){
        RestaurantInfo.Adress = adres;
        RestaurantInfo.Email = email;
        RestaurantInfo.Phone_number = phoneNumber;
    }

    public void ReservationOptions()
    {
        string answer = "";
        while(answer != "1"){
        do{
        System.Console.WriteLine("[1]: Home");
        System.Console.WriteLine("[2]: See all reservations");
        System.Console.WriteLine("[3]: See reservations on a certain date");
        System.Console.WriteLine("[4]: See reservations on a certain time and date");
        System.Console.WriteLine("[5]: Cancel reservation"); // Komt nog
        answer = Console.ReadLine().ToLower();
        if (answer == "1")
        {
            Home.Options();
            break;
        }
        } while (answer != "2" && answer != "3" && answer != "4" && answer != "5");
        SeeReservations(answer);
        }
    }
    public void SeeReservations(string answer)
    {
        switch (answer)
        {
            // Maak jullie geen zorgen de fouthandling van userinput ga ik later doen
            case "2":
            ReservationLogic.PrintAllReservations();
            break;
            case "3":
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
            case "4":
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
            case "5":
            System.Console.WriteLine("What is the guest Id?");
            int GuestId = Convert.ToInt32(Console.ReadLine());
            ReservationLogic.CancelReservation(GuestId);
            break;
        }
    }

    public void ReviewOptions()
    {
        string answer = "";
        while(answer != "1"){
        do{
        System.Console.WriteLine("[1]: Home");
        System.Console.WriteLine("[2]: See all reviews");
        System.Console.WriteLine("[3]: See reviews based on ratings");
        System.Console.WriteLine("[4] Delete review");
        System.Console.WriteLine("[5] Delete all reviews");
        answer = Console.ReadLine().ToLower();
        if (answer == "1")
        {
            Home.Options();
            break;
        }
        } while (answer != "2" && answer != "3" && answer != "4" && answer != "5");
        ReviewDetails(answer);
        }
    }

    public void ReviewDetails(string answer)
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
            ReviewLogic.DeleteReview();
            break;
            case "5":
            ReviewLogic.DeleteAllReviews();
            break;
        }  
    }
}
