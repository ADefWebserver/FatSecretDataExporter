using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fatsecret.platform.FatSecretWeight
{
    public class FatSecretWeight
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
        public string date_int { get; set; }
        public string weight_comment { get; set; }
        public string weight_kg { get; set; }
    }

    public class FatSecretWeightSingle
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
