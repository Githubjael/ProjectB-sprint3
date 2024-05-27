class TableForSix : Table
{
    public TableForSix(string id, int type) : base(id + "D"){
        if (type == 6)
        {
            Type = type;
        }
    }
}
