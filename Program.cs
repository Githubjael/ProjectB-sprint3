public class Program
{
    public static void Main()
    {
        //create all pages
        Page homePage = new Home();
        Page contactPage = new Contact();
        Page menuPage = new Menu();
        Page reservationPage = new Reservation();
        Page Reviews = new Reviews();
        
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