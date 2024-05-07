class TableForFour : Table
{
    public TableForFour(string id, int type) : base(id + "B"){
        Type = type;
    }
    public override void IsReserved()
    {
        Reserved = true;
    }
    public override void Cancelled()
    {
        Reserved = false;
    }
}
