class Reservation : Page
{
    public override string Name => "Reservation";

    public override void Options()
    {
        System.Console.WriteLine("[MR]: Make reservation");
        System.Console.WriteLine("[CR]: Cancel reservation");
        base.Options();
        //MAKE READLINE WORK
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
