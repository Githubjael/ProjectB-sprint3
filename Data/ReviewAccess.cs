// using Newtonsoft.Json;

// class ReviewAccess
// {
//     private static string fileName = @"Logic\Pages\Reviews.cs";

//     public static List<T> ReadFromJson()
//     {
//         try
//         {
//             using (StreamReader reader = new StreamReader(fileName))
//             {
//                 string json = reader.ReadToEnd();
//                 return JsonConvert.DeserializeObject<List<Review>>(json);
//             }
//         }
//         catch (FileNotFoundException)
//         {
//             Console.WriteLine("no reviews available");
//             return new List<Review>();
//         }
//     }

//     public static void WriteToJson(List<T> reviews)
//     {
//         try
//         {
//             using (StreamWriter writer = new StreamWriter(fileName))
//             {
//                 string json = JsonConvert.SerializeObject(reviews);
//                 writer.Write(json);
//             }
//         }
//         catch (IOException ex)
//         {
//             Console.WriteLine($"Error writing reviews file: {ex.Message}");
//             throw;
//         }
//     }
// }