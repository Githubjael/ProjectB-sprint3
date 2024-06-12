class ContactAccess
{
    private static string Filename = @"..\..\..\DataSources\Contact.json";
    public static List<ContactDataModel> ReadFromJson() => JsonStuff.ReadFromJson<ContactDataModel>(Filename);
    public static void WriteToJson(List<ContactDataModel> ContactInfo) => JsonStuff.WriteToJson(ContactInfo, Filename);
}
