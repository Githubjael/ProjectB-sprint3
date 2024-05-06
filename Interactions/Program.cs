public class Program
{
    public static void Main()
    {
        ReservedTable.PopulateTables();
        ReservationLogic.AddReservationFromJson(); 
        Home.Options();
    }
}
