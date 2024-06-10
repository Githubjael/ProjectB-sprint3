class TimeSlots
{
    public static string Filename = @"C:\Users\User\Desktop\Project B\DataSources\TimeSlots.Json";
    public static List<string> ReadFromJson() => JsonStuff.ReadFromJson<string>(Filename);
    public static void WriteToJson(List<string> timeSlots) => JsonStuff.WriteToJson(timeSlots, Filename);
}
