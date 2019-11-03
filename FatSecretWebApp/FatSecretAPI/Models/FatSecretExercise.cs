using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fatsecret.platform.FatSecretExercise
{
    public class FatSecretExercise
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
        public string date_int { get; set; }
    }

    public class FatSecretExerciseSingle
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
