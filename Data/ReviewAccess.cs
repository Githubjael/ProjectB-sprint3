using Newtonsoft.Json;

class ReviewAccess
{
    private static string fileName = @"..\..\..\DataSources\Reviews.Json"; //vergeet niet je eigen path te copy
    public static List<Review> ReadFromJson() => JsonStuff.ReadFromJson<Review>(fileName);
    public static void WriteToJson(List<Review> reviews) => JsonStuff.WriteToJson(reviews, fileName);
}
