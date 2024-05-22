class TableForSix : Table
{
    public TableForSix(string id, int type) : base(id + "C"){
        if (type == 6)
        {
            Type = type;
        }
    }
}
