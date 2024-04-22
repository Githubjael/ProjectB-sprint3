using Newtonsoft.Json;

class UsersAccess
{
    private static string fileName = @"C:\Users\Hilal\OneDrive\Bureaublad\eindelijk\ProjectB-sprint3-main\DataSources\users.json"; //vergeet niet je eigen path te copy

    public static List<Person> ReadFromJson()
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                List<Person> allusers = JsonConvert.DeserializeObject<List<Person>>(json);
                return allusers;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("no json file found");
            return new List<Person>();
        }
    }

    public static void WriteToJson(List<Person> users)
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

    public static void SaveUser(Person user)
    {
        var users = LoadUsers();
        users.Add(user); 
        SaveUsers(users);
    }

    private static List<Person> LoadUsers()
    {
        if (!File.Exists(fileName))
            return new List<Person>();

        using (StreamReader file = File.OpenText(fileName))
        {
            var serializer = new JsonSerializer();
            var users = (List<Person>)serializer.Deserialize(file, typeof(List<Person>));
            return users ?? new List<Person>();
        }
    }



    public static void SaveUsers(List<Person> users)
    {
        WriteToJson(users);
    }

    public static Person GetUser(string email)
    {
        var users = LoadUsers();
        return users.Find(u => u.EmailAddress == email);
    }
}
