using System.Runtime.CompilerServices;
using System.Security.Cryptography;
static class ReservedTable
{
    private static int _timeCount = 0;
    public static List<Table> TableTracker = new List<Table>() { }; // nodig om alle tafels op een lijstje te hebben en om staus binnen de tafels te veranderen
    public static void Increment()
    {
        _timeCount ++;
    }
    public static void Reset()
    {
        _timeCount = 0;
    }
    public static void PopulateTables() // deze moet in Reservations logische laag + info halen over gereserveerde tafels uit json
    {
        for (int h = 1; h <= 7; h++)
        {
            TableTracker.Add(new BarTable(Convert.ToString(h), 1));
        }
        for (int i = 8; i <= 15; i++)
        {
            TableTracker.Add(new TableForTwo(Convert.ToString(i), 2));
        }
        for (int j = 16; j <=  21; j++)
        {
            TableTracker.Add(new TableForFour(Convert.ToString(j), 4));
        }
        for (int k = 22; k <= 23; k++)
        {
            TableTracker.Add(new TableForSix(Convert.ToString(k), 6));
        }
    }
    public static int CheckAvailableTables(string Date, string TimeSlot) // Checks if tables are full, if so another method will remove the timeslot
    {
        MakeTablesAvailable(Date, TimeSlot);
        foreach(Table table in TableTracker)
        {
            if (ReservationLogic.CheckReservedTable(table.ID, Date, TimeSlot))
            {
                table.IsReserved();
                Increment();
            }
        }
        return _timeCount;
    }
    public static void MakeTablesAvailable(string Date, string TimeSlot) // Makes tables available after a user chose a timeslot
    { // Use this when you Assign Tables
        foreach(Table table in TableTracker)
        {
            if(!ReservationLogic.CheckReservedTable(table.ID, Date, TimeSlot))
            {
                table.Cancelled();
            }
        }
    }
   public static List<string> GetTimes(string Date)
{
    List<string> Times = TimeSlots.ReadFromJson();
    // Als timelist verandered kunnen we het gewoon sturen als parameter

    for (int i = Times.Count - 1; i >= 0; i--)
    {
        Reset();
        if (CheckAvailableTables(Date, Times[i]) >= TableTracker.Count)
        {
            Times.RemoveAt(i);
        }
        else if (DateTime.Parse($"{Date} {Times[i]}") < DateTime.Now)
        {
            Times.RemoveAt(i);
        }
    }

    return Times;
}
    public static List<string> AssignTable(int AmountOfGuests, string Date, string TimeSlot) // the user already chose a Date and TimeSlot
    {
        List<string> ChosenTable = new (){};
        MakeTablesAvailable(Date, TimeSlot);
        int TableType = AmountOfGuests switch
        {
            1 => 1,
            2 => 2,
            3 => 4,
            4 => 4,
            5 => 6,
            6 => 6,
        };
        try{
        var table = TableTracker.Find(x => x.Type == TableType && !x.Reserved);
        table.IsReserved();
        ChosenTable.Add(table.ID);
        }
        catch (Exception)
        {
            ReservationLogic.SwitchIfNull(TableType);
        } 
        return ChosenTable;
    }
 
}
