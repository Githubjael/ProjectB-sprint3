using Newtonsoft.Json;

class ManagerAccess
{
    private static string fileName = @"..\..\..\DataSources\Manager.json"; //vergeet niet je eigen path te copy

    public static List<Manager> ReadFromJson() => JsonStuff.ReadFromJson<Manager>(fileName);
}
