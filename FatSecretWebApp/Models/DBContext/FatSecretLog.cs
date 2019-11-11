using System;
using System.Collections.Generic;

namespace FatSecretWebApp.Models.DBContext
{
    public partial class FatSecretLog
    {
        public int Id { get; set; }
        public string LogTexxt { get; set; }
        public string GoogleUserName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}