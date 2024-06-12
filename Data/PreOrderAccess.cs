class PreOrderAccess
{
    private static string FileName = @"..\..\..\DataSources\PreOrders.Json";
    public static List<PreOrder> ReadFromJson() => JsonStuff.ReadFromJson<PreOrder>(FileName);
    public static void WriteToJson(List<PreOrder> PreOrders) => JsonStuff.WriteToJson(PreOrders, FileName);
}
