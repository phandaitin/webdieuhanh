using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Areas.Admin.Data
{
    public partial class TLaixe
    {
        public int LaixeId { get; set; }
        public string Hoten { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public DateTime? Dob { get; set; }
        public string MaDv { get; set; }
    }
}
