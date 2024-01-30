using System;
using System.Collections.Generic;

#nullable disable

namespace VNETC_WebApp.Areas.Admin.Data
{
    public partial class TPost
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string Tencongtrinh { get; set; }
        public string Chudautu { get; set; }
        public string Website { get; set; }
        public string Content { get; set; }
        public string Thumb1 { get; set; }
        public string Thumb2 { get; set; }
        public string Thumb3 { get; set; }
        public string Slug { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string Author { get; set; }
    }
}
