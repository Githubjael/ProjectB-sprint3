static class ContactOptions
{
        public static void Options()
    {
        SeeContactInfo();
        Console.WriteLine("[1]: Home");
        Console.WriteLine(""); 

        while (true)
        {
            string userChoice = Console.ReadLine().ToUpper();

            switch (userChoice)
            {
                case "1":
                    Home.Options();
                    return;

                default:
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid input. Please try again."); Console.ResetColor();
                    break;
            }
        }
    }
    public static void SeeContactInfo()
    {
        ContactDataModel restaurantInfo = ContactAccess.ReadFromJson()[0];
        Console.WriteLine("╔══════════════════════ CONTACT INFORMATION ════════════════════════╗");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("                     Welcome to Jane's Restaurant!               ");
        System.Console.WriteLine("               Please find our contact information below               ");
        System.Console.WriteLine(" ");
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine($"                      ⚲ Address: {restaurantInfo.Adress}");
        System.Console.WriteLine($"                     ☏ Phone number: {restaurantInfo.PhoneNumber}");
        System.Console.WriteLine($"                  ✉ Email: {restaurantInfo.Email}");
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine("╚═══════════════════════════════════════════════════════════════════╝");
    }
}
