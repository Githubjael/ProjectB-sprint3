class ContactAccess
{
    public static string Filename = @"C:\Users\User\Desktop\Project B\DataSources\Contact.Json";
    public static List<ContactDataModel> ReadFromJson() => JsonStuff.ReadFromJson<ContactDataModel>(Filename);
    public static void WriteToJson(List<ContactDataModel> ContactInfo) => JsonStuff.WriteToJson(ContactInfo, Filename);
}
