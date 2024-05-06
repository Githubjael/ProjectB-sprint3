using Newtonsoft.Json;

class ManagerAccess
{
    private static string fileName = @"C:\Users\User\Desktop\Project B\DataSources\Manager.Json"; //vergeet niet je eigen path te copy

    public static List<Manager> ReadFromJson()
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                List<Manager> allusers = JsonConvert.DeserializeObject<List<Manager>>(json);
                return allusers;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("no json file found");
            return new List<Manager>();
        }
    }
}
