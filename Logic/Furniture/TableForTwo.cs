class TableForTwo : Table
{
    public TableForTwo(string id, int type) : base(id + "A"){
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
