class TableForSix : Table
{
    public TableForSix(string id, int type) : base(id + "C"){
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
