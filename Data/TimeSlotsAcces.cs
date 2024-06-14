class TimeSlots
{
    public static string Filename = @"..\..\..\DataSources\TimeSlots.Json";
    public static List<string> ReadFromJson() => JsonStuff.ReadFromJson<string>(Filename);
    public static void WriteToJson(List<string> timeSlots) => JsonStuff.WriteToJson(timeSlots, Filename);
}
