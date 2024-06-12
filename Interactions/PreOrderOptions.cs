static class PreOrderOptions
{
    public static List<PreOrder> PreOrders = PreOrderAccess.ReadFromJson();
    public static void AskDish(string GuestID, string Date, string Time)
    {
        // User typt 'D' als ie klaar is met bestelling
        // Als ie 'D' typt dan eh ja
        List<string> Dishes = new(); 
        Menu.DisplayMenu("Price");
        System.Console.WriteLine();
        string Dish = "";
        System.Console.WriteLine();
        System.Console.WriteLine("Type the id of the menu item you'd wish to order.\nType 'D' if you're done.");
        System.Console.WriteLine();
        while (!Dish.Equals("d", StringComparison.CurrentCultureIgnoreCase))
        {
        // Vraag docent of ie wilt dat klant meerdere dishes in 1 console.Readline wilt kunnen bestellen of niet
        // Gebruik lambda om dish te vinden in menu
        System.Console.WriteLine("Type the Id of the dish you'd like to order: ");
        Dish = Console.ReadLine();
        if (Dish != null && Dish.ToLower() != "d"){
            Dishes.Add(PreOrdering.menuItems.Find(menuItem=> menuItem.Id == Dish).Name);
        }
    }
    List<PreOrder> Ordered = PreOrderAccess.ReadFromJson();
    if (Ordered == null || Ordered.Count == 0)
        {Ordered = new(){};} // null?
        PreOrder preOrder = new(GuestID, Dishes, Date, Time);
        Ordered.Add(preOrder);
        PreOrderAccess.WriteToJson(Ordered); 
}

    public static void CancelPreOrder()
    {
        System.Console.WriteLine("What is your guest ID?");
        string GuestID = Console.ReadLine();
        if (PreOrders == null || PreOrders.Count == 0)
        {
            System.Console.WriteLine("No pre-orders found");
        }
        else
        {
            var preOrder = PreOrders.Find(order => order.GuestID == GuestID);
            if (preOrder == null)
            {
                System.Console.WriteLine("No pre-orders found");
            }
            else
            {
                PreOrders.Remove(preOrder);
                PreOrderAccess.WriteToJson(PreOrders);
                System.Console.WriteLine("Your order is successfully cancelled!");
            }
        }
    }

    public static void ViewPreOrders()
    {
        System.Console.WriteLine("What is your guest Id?");
        string GuestID = Console.ReadLine();
        var preOrder = PreOrders.Find(x => x.GuestID == GuestID);
        System.Console.WriteLine();
        if (preOrder != null)
        {
            System.Console.WriteLine("=======================");
            System.Console.WriteLine(preOrder.Date);
            System.Console.WriteLine(preOrder.Time);
            foreach (var order in preOrder.Dishes)
            {
                System.Console.WriteLine(order);
                System.Console.WriteLine(PreOrdering.menuItems.Find(x => x.Name == order).Price);
            }
        }
        else{
            System.Console.WriteLine("pre order not found!");
        }
    }
}
