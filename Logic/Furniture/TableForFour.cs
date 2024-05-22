class TableForFour : Table
{
    public TableForFour(string id, int type) : base(id + "B"){
        if (type == 2)
        {
            Type = type;
        }
    }
}
