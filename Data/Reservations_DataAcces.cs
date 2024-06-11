using Newtonsoft.Json;

class ReservationDataAccess
{
    public static string fileName = @"..\..\..\DataSources\Reservations.Json";
    public static List<ReservationDataModel> ReadFromJson() => JsonStuff.ReadFromJson<ReservationDataModel>(fileName);
    public static void WriteToJson(List<ReservationDataModel> ReservationList) => JsonStuff.WriteToJson(ReservationList, fileName);
}
