static class PreOrdering
{
    public static List<MenuItem> menuItems = Menu.menuItems;

    public static PreOrder AskDish(string GuestID, string Date, string Time) => PreOrderOptions.AskDish(GuestID, Date, Time);
    public static void CancelPreOrder() => PreOrderOptions.CancelPreOrder();

    public static void ViewPreOrders() => PreOrderOptions.ViewPreOrders();

}
