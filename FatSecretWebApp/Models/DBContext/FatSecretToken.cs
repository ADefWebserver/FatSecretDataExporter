using System;
using System.Collections.Generic;

namespace FatSecretWebApp.Models.DBContext
{
    public partial class FatSecretToken
    {
        public int Id { get; set; }
        public string GoogleUserName { get; set; }
        public string FatSecretUserToken { get; set; }
        public string FatSecretAuthSecret { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}