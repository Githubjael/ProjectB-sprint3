using Newtonsoft.Json;

class ReservationDataAccess
{
    public static string fileName = @"..\..\..\DataSources\Reservations.Json";

    // voor nu schrijven we alle reservations naar json
    // er wordt nog niet nagekeken of de dagen/maanden/tafels etc etc available zijn 
    // en reservations kunnen nog niet worden geannuleerd
    public static List<ReservationDataModel> ReadFromJson() // Alle reserveringen lezen en in een lijst stoppen
    {
        using StreamReader reader = new(fileName);
        var json = reader.ReadToEnd();
        List<ReservationDataModel> AllReservations = JsonConvert.DeserializeObject<List<ReservationDataModel>>(json);
        return AllReservations;
        // in logische laag moet je kijken of er een tafelID is
    }
    public static void WriteToJson(List<ReservationDataModel> ReservationList)
    {
        // write to json
        StreamWriter writer = new(fileName);
        string List2Json = JsonConvert.SerializeObject(ReservationList);
        writer.Write(List2Json);
        writer.Close();
        // write to json
    }
}