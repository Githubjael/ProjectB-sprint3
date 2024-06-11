using System;
using System.Text;
public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        ReservedTable.PopulateTables();
        ReservationLogic.AddReservationFromJson(); 
        Home.Options();
    }
}
