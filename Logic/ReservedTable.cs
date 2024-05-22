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
        for (int i = 1; i <= 7; i++)
        {
            TableTracker.Add(new BarTable(Convert.ToString(i), 1));
        }
        for (int j = 8; j <= 16; j++)
        {
            TableTracker.Add(new TableForTwo(Convert.ToString(j), 2));
        }
        for (int k = 17; k <= 21; k++)
        {
            TableTracker.Add(new TableForFour(Convert.ToString(k), 4));
        }
        for (int l = 22; l <= 23; l++)
        {
            TableTracker.Add(new TableForSix(Convert.ToString(l), 6));
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
    List<string> Times = new List<string> { "10:00", "17:00", "22:00" };
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
    public static List<Table> AssignTable(int AmountOfGuests, string Date, string TimeSlot) // the user already chose a Date and TimeSlot
    {
        List<Table> ChosenTable = new (){};
        MakeTablesAvailable(Date, TimeSlot);
        int TableType = AmountOfGuests switch
        {
            1 => 2,
            2 => 2,
            3 => 4,
            4 => 4,
            5 => 6,
            6 => 6,
        };
        if (TableType == 2)
        {
            try{
            var table = TableTracker.Find(x => x.Type == TableType && !x.Reserved);
            table.IsReserved();
            ChosenTable.Add(table);
            }
            catch (Exception)
            {
                ReservationLogic.SwitchIfNull(TableType);
            }
        }
        else if (TableType == 4)
        {
            try{
            var table = TableTracker.Find(x => x.Type == TableType && !x.Reserved);
            table.IsReserved();
            ChosenTable.Add(table);
            }
            catch (Exception)
            {
                ReservationLogic.SwitchIfNull(TableType);
            }
        }
        else if (TableType == 6)
        {
            try{
            var table = TableTracker.Find(x => x.Type == TableType && !x.Reserved);
            table.IsReserved();
            ChosenTable.Add(table);
            }
            catch (Exception)
            {
                ReservationLogic.SwitchIfNull(TableType);
            } 
        }
        return ChosenTable;
    }
 public static List<Table> AssignTables(int AmountOfGuests, string Date, string TimeSlot)
    {
        MakeTablesAvailable(Date, TimeSlot);
            // tableAssignments.Add(guestID, tableID);
            // Maak hier een functie van in ReservedTable.cs!!!!!!
            List<Table> ChosenTables = new List<Table>();
                int ToBeSeated = AmountOfGuests;
                List<int> TableTypes = new List<int>()
                {
                    Capacity = 2
                };
                // bool Loop = true;
                int ToReserve;
                do
                {
                    if (ToBeSeated >= 6)
                    {
                        Console.WriteLine($"Choose a Table to reserve(1-3):");
                        Console.WriteLine($"1) 2 persons table");
                        Console.WriteLine($"2) 4 persons table");
                        Console.WriteLine($"3) 6 persons table");
                    }
                    else if (ToBeSeated >= 3)
                    {
                        Console.WriteLine($"Choose a Table to reserve(1-3):");
                        Console.WriteLine($"1) 2 persons table");
                        Console.WriteLine($"2) 4 persons table");
                    }
                    else if (ToBeSeated >= 1)
                    {
                        Console.WriteLine($"Choose a Table to reserve(1-3):");
                        Console.WriteLine($"1) 2 persons table");
                    }
                    string answer = Console.ReadLine();
                    if (string.IsNullOrEmpty(answer))
                    {
                        Console.WriteLine("Invalid input. You must enter '1', '2', '3' .");
                    }
                    else if(answer != "1" && answer != "2" && answer != "3")
                    {
                        Console.WriteLine("Invalid input. You must enter '1', '2', '3' .");
                    }
                    if (answer == "3")
                    {
                        ToBeSeated -= 6;
                        Console.WriteLine("Thank you!");
                        ToReserve = Convert.ToInt16(answer);
                        TableTypes.Add(ToReserve);
                    }
                    else if (answer == "2")
                    {
                        ToBeSeated -= 4;
                        Console.WriteLine("Thank you!");
                        ToReserve = Convert.ToInt16(answer);
                        TableTypes.Add(ToReserve);
                    }
                    else if(answer == "1")
                    {
                        ToBeSeated -= 2;
                        Console.WriteLine("Thank you!");
                        ToReserve = Convert.ToInt16(answer);
                        TableTypes.Add(ToReserve);
                    }
                } while(ToBeSeated > 0);
                foreach(int type in TableTypes)
                {
                    var tabletype = type switch
                    {
                        1 => 2,
                        2 => 4,
                        3 => 6,
                    };
                    var found = TableTracker.Find(x => x.Type == tabletype && !x.Reserved);
                    try{
                        found.IsReserved();
                        ChosenTables.Add(found);
                    }
                    catch(Exception)
                    {
                        var tables = ReservationLogic.SwitchIfNull(tabletype);
                        foreach(Table table in tables)
                        {
                            ChosenTables.Add(table);
                        }
                    }
                }
                return ChosenTables;
    }
}
