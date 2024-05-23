using Newtonsoft.Json;

class ManagerAccess
{
    private static string fileName = @"C:\Users\User\Desktop\Project B\DataSources\Manager.Json"; //vergeet niet je eigen path te copy

    public static List<Manager> ReadFromJson() => JsonStuff.ReadFromJson<Manager>(fileName);
}
