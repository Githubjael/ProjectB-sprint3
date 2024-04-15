class Reservation : Page
{
    public override string Name => "Reservation";

    public override void Options()
    {
        System.Console.WriteLine("[1]: Make reservation");
        System.Console.WriteLine("[2]: Cancel reservation");
        base.Options();
    }

    public void MakeReservation()
    {

    }

    public void CancelReservation()
    {
        
    }
    public override void Contents()
    {
    }
}
