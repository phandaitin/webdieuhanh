using System;
using System.Collections.Generic;

#nullable disable

namespace VNETC_WebApp.Areas.Admin.Data
{
    public partial class TUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
