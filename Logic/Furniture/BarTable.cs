class BarTable : Table{
    public BarTable(string id, int type) : base(id + "A"){
        if (type == 1)
        {
            Type = type;
        }
    }
}
