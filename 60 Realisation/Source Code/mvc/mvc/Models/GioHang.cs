using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Models
{
    public class GioHang
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();

        public int iMaSP { get; set; }
        public string iTenSP { get; set; }
        public string iAnhBia { get; set; }
        public double iDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double iThanhTien { get { return iSoLuong * iDonGia; } }

        public GioHang(int Ma)
        {
            iMaSP = Ma;
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            iTenSP = sach.TenSach;
            iAnhBia = sach.AnhBia;
            iDonGia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
    
}
