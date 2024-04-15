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
        
        //begin program
        homePage.Options();
    }
}