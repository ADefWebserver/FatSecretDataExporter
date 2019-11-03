using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace fatsecret.platform
{
    public class FatSecretProfile
    {
        public Profile profile { get; set; }
    }

    public class Profile
    {
        public string goal_weight_kg { get; set; }
        public string height_cm { get; set; }
        public string height_measure { get; set; }
        public object last_weight_comment { get; set; }
        public string last_weight_date_int { get; set; }
        public string last_weight_kg { get; set; }
        public string weight_measure { get; set; }
    }
}
