class PreOrderAccess
{
    public static string FileName = @"C:\Users\User\Desktop\Project B\DataSources\PreOrders.Json";
    public static List<PreOrder> ReadFromJson() => JsonStuff.ReadFromJson<PreOrder>(FileName);
    public static void WriteToJson(List<PreOrder> PreOrders) => JsonStuff.WriteToJson(PreOrders, FileName);
}
