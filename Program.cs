public class Program
{
    public static void Main()
    {
        //create all pages
        Page homePage = new Home();
        Page ContactPage = new Contact();
        Page ReservationPage = new Reservation();
        
        bool Loop = true; // loop bool
        do{
            //begin program
            homePage.Options();
            var UserInput = Console.ReadLine();
            if (UserInput.ToLower() == "e" || UserInput.ToLower() == "exit"){
                Loop = false;
            }
        } while(Loop);
    }
}
