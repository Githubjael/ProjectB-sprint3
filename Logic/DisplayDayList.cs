using System.Globalization;

class DisplayDayList{
    // hier komen alle tijdstippen. Restaurant is open van 10:00 tot 17:00
    public static List<string> DayList = new(){
        "10:00", "10:30", "11:00",
        "11:30", "12:00", "12:30",
        "13:00", "13:30", "14:00",
        "14:30", "15:00", "15:30",
        "16:00", "16:30", "17:00"
    };

    public static List<string> GiveListBasedOnDay(int day, int month){
        List<string> TimeList = new(){};
        foreach(string Time in DayList)
        {
            TimeList.Add(Time);
        }
for (int i = 0; i < TimeList.Count; i++)
{
    string Time = $"{day}-0{month}-2024 {TimeList[i]}";
    try
    {
        DateTime hourToCompare = DateTime.ParseExact(Time, "dd-MM-yyyy HH:mm", CultureInfo.GetCultureInfo("nl-NL"));
        if (hourToCompare < DateTime.Now)
        {
            TimeList.RemoveAt(i);
            // After removing an element, adjust the index to account for the removed item
            i--;
        }
    }
    catch (FormatException)
    {
        // Handle invalid time format
        Console.WriteLine($"Invalid time format: {TimeList[i]}");
    }
}

        ReservedTable.CheckIfTableReserved(day, month);
        if (TimeList.Count == 0)
        {
            DisplayMonthList.GiveListBasedOnMonth(month).Remove(day);
        }
        return TimeList;
    }

}
