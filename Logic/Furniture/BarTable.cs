class BarTable : Table{
    public BarTable(string id, int type) : base(id + "D"){
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
