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
        ReservedTable.CheckIfTableReserved(day, month);
        if (TimeList.Count == 0)
        {
            DisplayMonthList.GiveListBasedOnMonth(month).Remove(day);
        }
        return TimeList;
    }

}
