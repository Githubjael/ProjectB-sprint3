static class ReservedTable
{

    public static List<Table> TableTracker = new List<Table>() { }; // nodig om alle tafels op een lijstje te hebben en om staus binnen de tafels te veranderen

    public static void PopulateTables() // deze moet in Reservations logische laag + info halen over gereserveerde tafels uit json
    {
        for (int i = 1; i <= 8; i++)
        {
            TableTracker.Add(new TableForTwo(Convert.ToString(i), 2));
        }
        for (int j = 9; j <= 14 ; j++)
        {
            TableTracker.Add(new TableForFour(Convert.ToString(j), 4));
        }
        for (int k = 15; k <= 16; k++)
        {
            TableTracker.Add(new TableForSix(Convert.ToString(k), 6));
        }
    }
    public static List<Table> AssignTable(int AmountOfGuests)
    {
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
                    if (found is null)
                    {
                        var tables = ReservationLogic.SwitchIfNull(found, tabletype);
                        foreach(Table table in tables)
                        {
                            ChosenTables.Add(table);
                        }
                    }
                    else{
                    found.IsReserved(); // Waarom is dit steeds null?
                    ChosenTables.Add(found);
                    }
                }
            return ChosenTables;
    }
    public static void CheckIfTableReserved(int day, int month, int year){
        // zet tafels in alle dagen van het jaar op vol als ze dat zijn
        int TimeCount = 0; // kijkt of tijdstip vol is
    List<string> TimeList = new(){
        "10:00", "10:30", "11:00",
        "11:30", "12:00", "12:30",
        "13:00", "13:30", "14:00",
        "14:30", "15:00", "15:30",
        "16:00", "16:30", "17:00"
    };
        foreach(string Time in TimeList)
        {
        foreach (Table table in TableTracker)
        {
            // kijk hier na of alle tafles op die tijdstip vol zijn
            if (ReservationLogic.CheckReservedTable(table.ID, $"{day}/{month}/{year}", Time))
            {
                table.IsReserved();
                TimeCount++; //checked aantal gereserveerde tafels op die dag
            }
            if (TimeCount >= TableTracker.Count){
                DisplayDayList.GiveListBasedOnDay(day, month, year).Remove(Time);
               //Remove from list 
            }
            // if (DayCount >= TableTracker.Count)
            // {
            //     DisplayMonthList.MonthList.Remove(day);
            // } ----> GEBEURD IN DisplayDayList
        }
        }
    }   

} 
