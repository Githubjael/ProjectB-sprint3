using Newtonsoft.Json;
class JsonStuff
{
    public static List<T> ReadFromJson<T>(string FileName) // Alle reserveringen lezen en in een lijst stoppen
    {
        using StreamReader reader = new(FileName);
        var json = reader.ReadToEnd();
        List<T> GenericList = JsonConvert.DeserializeObject<List<T>>(json);
        return GenericList;
        // in logische laag moet je kijken of er een tafelID is
    }


    public static void WriteToJson<T>(List<T> ReservationList, string fileName)
    {
        // write to json
        StreamWriter writer = new(fileName);
        string List2Json = JsonConvert.SerializeObject(ReservationList, Formatting.Indented);
        writer.Write(List2Json);
        writer.Close();
        // write to json
    }
}
