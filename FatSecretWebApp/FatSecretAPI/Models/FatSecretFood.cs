using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fatsecret.platform.FatSecretFood
{
    public class FatSecretFood
    {
        public Month month { get; set; }
    }

    public class Month
    {
        public List<Day> day { get; set; }
        public string from_date_int { get; set; }
        public string to_date_int { get; set; }
    }

    public class Day
    {
        public string calories { get; set; }
        public string carbohydrate { get; set; }
        public string date_int { get; set; }
        public string fat { get; set; }
        public string protein { get; set; }
    }

    public class FatSecretFoodSingle
    {
        public MonthSingle month { get; set; }
    }

    public class MonthSingle
    {
        public Day day { get; set; }
        public string from_date_int { get; set; }
        public string to_date_int { get; set; }
    }
}
