using System;
using System.Collections.Generic;

#nullable disable

namespace WebApp.Areas.Admin.Data
{
    public partial class TXe
    {
        public int XeId { get; set; }
        public string Soxe { get; set; }
        public string Loaixe { get; set; }
        public DateTime? NgayDangKiem { get; set; }
        public DateTime? HanBaoHiem { get; set; }
        public DateTime? NgayDaiTuGanNhat { get; set; }
        public string Tinhtrang { get; set; }
        public string MaDv { get; set; }
        public string LaixeId { get; set; }
        public string Ghichu { get; set; }
    }
}
