using Newtonsoft.Json;

class UsersAccess
{
    private static string fileName = @"C:\Users\User\Desktop\Project B\DataSources\Users.Json"; //vergeet niet je eigen path te copy

    public static List<Guest> ReadFromJson() => JsonStuff.ReadFromJson<Guest>(fileName);

    public static void WriteToJson(List<Guest> users) => JsonStuff.WriteToJson(users, fileName);

    public static void SaveUser(Guest user)
    {
        var users = LoadUsers();
        users.Add(user); 
        SaveUsers(users);
    }

    private static List<Guest> LoadUsers()
    {
        if (!File.Exists(fileName))
            return new List<Guest>();

        using (StreamReader file = File.OpenText(fileName))
        {
            var serializer = new JsonSerializer();
            var users = (List<Guest>)serializer.Deserialize(file, typeof(List<Guest>));
            return users ?? new List<Guest>();
        }
    }



    public static void SaveUsers(List<Guest> users)
    {
        WriteToJson(users);
    }

    public static Guest GetUser(string email)
    {
        var users = LoadUsers();
        return users.Find(u => u.EmailAddress == email);
    }
}
