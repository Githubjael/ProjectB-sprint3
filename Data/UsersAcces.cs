using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

class UsersAccess
{
    private static string fileName = @"C:\Users\Hilal\OneDrive\Bureaublad\eindelijk\ProjectB-sprint3-main\DataSources\users.json"; // copy je eige path.

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
        using (StreamWriter file = File.CreateText(fileName))
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(file, users);
        }
    }

    public static Guest GetUser(string email)
    {
        var users = LoadUsers();
        return users.Find(u => u.EmailAddress == email);
    }

    public static void UpdateUser(Guest updatedUser)
    {
        var users = LoadUsers();
        var index = users.FindIndex(u => u.EmailAddress == updatedUser.EmailAddress);
        if (index != -1)
        {
            users[index] = updatedUser;
            SaveUsers(users);
        }
        else
        {
            Console.WriteLine($"User {updatedUser.EmailAddress} not found.");
        }
    }
}
