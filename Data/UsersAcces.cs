using Newtonsoft.Json;

class UsersAccess
{
    private static string fileName = @"..\..\..\DataSources\Users.Json"; //vergeet niet je eigen path te copy

    public static List<Guest> ReadFromJson()
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                List<Guest> allusers = JsonConvert.DeserializeObject<List<Guest>>(json);
                return allusers;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("no json file found");
            return new List<Guest>();
        }
    }

    public static void WriteToJson(List<Guest> users)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                writer.Write(json);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error writing users file: {ex.Message}");
            throw;
        }
    }

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
