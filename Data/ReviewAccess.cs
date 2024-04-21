using Newtonsoft.Json;

class ReviewAccess
{
    private static string fileName = @"C:\Users\Hilal\OneDrive\Bureaublad\imdone\ProjectB-sprint3-main\DataSources\Reviews.Json"; //vergeet niet je eigen path te copy

    public static List<Review> ReadFromJson()
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                List<Review> allreviews = JsonConvert.DeserializeObject<List<Review>>(json);
                return allreviews;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("no json file found");
            return new List<Review>();
        }
    }

    public static void WriteToJson(List<Review> reviews)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                string json = JsonConvert.SerializeObject(reviews);
                writer.Write(json);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error writing reviews file: {ex.Message}");
            throw;
        }
    }
}
