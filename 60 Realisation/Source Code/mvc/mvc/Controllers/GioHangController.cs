using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class GioHangController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            //Tao list gio hang de luu lai gio hang.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //neu khong co gio hang
            if (lstGioHang == null)
            {
                //tao mot gio hang moi
                lstGioHang = new List<GioHang>();
                //gan lai gio hang.
                Session["GioHang"] = lstGioHang;

            }
            //xuat ra gio hang.
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int Ma, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ra gio hang.
            List<GioHang> lstGioHang = LayGioHang();
            //tim sach trong gio hang.
            GioHang gh = lstGioHang.Find(n => n.iMaSP == Ma);
            if (gh == null)
            {
                gh = new GioHang(Ma);
                //add san pham moi them vao list.
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.iSoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhatGioHang(int Ma, FormCollection f)
        {
            // tao mot ma san pham theo ma sach.
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            //xem thu ma sach do co chua.
            if (sach == null)
            {
                //neu khong co thy thong bao loi.
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang ra.
            List<GioHang> lstGioHang = LayGioHang();
            //xem thu ma san pham co chua.
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == Ma);
            //neu ton tai thy cho chih sua.
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int Ma)
        {

            // tao mot ma san pham theo ma sach.
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            //xem thu ma sach do co chua.
            if (sach == null)
            {
                //neu khong co thy thong bao loi.
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang ra.
            List<GioHang> lstGioHang = LayGioHang();
            //xem thu ma san pham co chua.
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == Ma);
            //neu ton tai thy cho chih sua.
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == Ma);

            }
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("index", "Home"); 
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        public int TongSoLuong()
        {
            int iTongSoLuong = 0;
            //Tao list gio hang de luu lai gio hang.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //neu khong co gio hang
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        public double TongTien()
        {
            double iTongTien = 0;
            //Tao list gio hang de luu lai gio hang.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //neu khong co gio hang
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n=>n.iThanhTien);
            }
            return iTongTien;
        }
        public PartialViewResult GioHangPartial()
        {
            if(TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }
        public ActionResult DatHang()
        {
            //xem thu da dang nhap chua.
            if(Session["TaiKhoan"] == null || Session["TaiKhoan"] == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //xem thu trong gio hang co gio hang nao chua.
            if (Session["GioHang"]== null)
            {
                return RedirectToAction("index","Home");
            }
            //them don hang.
            //tao ra don hang
            DonHang dh = new DonHang();
            //khach hang mua don hang la ai.
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            //lay don hang trong gio hang.
            List<GioHang> gh = LayGioHang();
            //tao don hang voi ma khach hang.
            dh.MaDonHang = kh.MaKH;
            //ngay mua hang.
            dh.NgayDat = DateTime.Now;
            //them don hang vao csdl.
            db.DonHangs.Add(dh);
            db.SaveChanges();
            //them chi tiet don hang.
            foreach(var item in gh)
            {
                //tao chi tiet don hang.
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDonHang = dh.MaDonHang;
                ctdh.MaSach = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.iDonGia;
                db.ChiTietDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            return RedirectToAction("index", "Home");
        }
    }

}