class DisplayMonthList
{
    // Hier ga ik er al vanuit dat de maanden possible zijn (als in niet voorbij, ik kan bijvoorbeeld niet in february 2024 gaan boeken)

    // Maandenlijst, later gaat elke maand zn lijst krijgen
    // Elke x wanneer een dag vol is, dan gaat de nummer van die dag uit de lijst.
    // Zelfde geld voor AvailableHours, elke dag zal zijn eigen lijst hebben
    public static List<int> MonthList = new List<int>(){
                1, 2, 3, 4, 5, 6, 7,
                8, 9, 10, 11, 12, 13, 14,
                15, 16, 17, 18, 19, 20, 21,
                22, 23, 24, 25, 26, 27, 28,
                29, 30, 31
            };
    public static List<int> GiveListBasedOnMonth(int Month)
    {
        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
        {
            MonthList = new List<int>(){
                1, 2, 3, 4, 5, 6, 7,
                8, 9, 10, 11, 12, 13, 14,
                15, 16, 17, 18, 19, 20, 21,
                22, 23, 24, 25, 26, 27, 28,
                29, 30, 31
            };
            if (MonthList.Count != 0){
            if (Month == DateTime.Now.Month){
            foreach(int dag in MonthList.ToList()){
                if (dag <= DateTime.Now.Day)
                {
                    MonthList.Remove(dag);
                }
            }
            }
            }
            return MonthList;
        }
        else if (Month == 2)
        {
            MonthList = new List<int>(){
                1, 2, 3, 4, 5, 6, 7,
                8, 9, 10, 11, 12, 13, 14,
                15, 16, 17, 18, 19, 20, 21,
                22, 23, 24, 25, 26, 27, 28, 29
            };
            if (DateTime.DaysInMonth(DateTime.Now.Year ,Month) == 28){
                MonthList = new List<int>(){
                1, 2, 3, 4, 5, 6, 7,
                8, 9, 10, 11, 12, 13, 14,
                15, 16, 17, 18, 19, 20, 21,
                22, 23, 24, 25, 26, 27, 28 
                };
            }
            if (MonthList.Count != 0){
            if (Month == DateTime.Now.Month){
            foreach(int dag in MonthList.ToList()){
                if (dag <= DateTime.Now.Day)
                {
                    MonthList.Remove(dag);
                }
            }
            }
            }
            return MonthList;
        }
        else{
            MonthList = new List<int>(){
                1, 2, 3, 4, 5, 6, 7,
                8, 9, 10, 11, 12, 13, 14,
                15, 16, 17, 18, 19, 20, 21,
                22, 23, 24, 25, 26, 27, 28, 29,
                30
            };
            if (MonthList.Count != 0){
            if (Month == DateTime.Now.Month){
            foreach(int dag in MonthList.ToList()){
                if (dag < DateTime.Now.Day) // Hier hele datums in doen blijkbaar
                {
                    MonthList.Remove(dag);
                }
                else if (dag == DateTime.Now.Day && DateTime.Now.Hour >= 17)
                {
                    MonthList.Remove(dag);
                }
            }
            }
            }
            return MonthList;
    }
    }
}
