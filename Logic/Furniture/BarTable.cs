class BarTable : Table{
    public BarTable(string id, int type) : base(id + "D"){
        if (type == 1)
        {
            Type = type;
        }
    }
}
