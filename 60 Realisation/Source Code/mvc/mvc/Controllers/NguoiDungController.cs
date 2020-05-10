using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKi(KhachHang kh)
        {//neu hop le thy cho them vao
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
            }
            
            return View();
        }
        [HttpGet]
        
        public ActionResult DangNhap()
        {
            
            return View();
        }
        [HttpPost]

        public ActionResult DangNhap(FormCollection f)
        {
            string tk = f["txtTaiKhoan"].ToString();
            string mk = f["txtMatKhau"].ToString();
            if(tk == "" || mk == "")
            {
                ViewBag.ThongBao = "Bạn Chưa Nhập Tài Khoản Hoặc Mật Khẩu.";
                return View();
            }

            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tk && n.MatKhau == mk);
            if(kh != null)
            {
                ViewBag.ThongBao = "Chúc Mừng Bạn Đã Đăng Nhập Thành Công.";
                Session["TaiKhoan"] = kh;
                return View();
            }
            
            ViewBag.ThongBao = "Tên Tài Khoản Hoặc Mật Khẩu Không ĐÚng.";
            return View();
        }
    }
}