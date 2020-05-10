using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace mvc.Controllers
{
    public class AdminController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();

        // GET: Admin
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;

            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNum, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MachuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        /*
        [HttpPost]
        public ActionResult ThemMoi(Sach sach,HttpPostedFileBase  fileUpload)
        {
            //luu ten cua file.

            var fileName = Path.GetFileName(fileUpload.FileName);

            //luu duong dan cua file.
            var path = Path.Combine(Server.MapPath("~/HinhAnhSP"),fileName);

            if (System.IO.File.Exists(path))
            {
               ViewBag.ThongBao = "Hình Ảnh đã tồn tại.";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            ViewBag.MachuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            return View();
        }
        */



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
        {


            //dua du lieu vao dropdowlist.
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            //kiem tra duong dan anh bia.
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "ChỌN hình Ảnh";
                return View();
            }

            //them vao co so du lieu.
            if (ModelState.IsValid)
            {
                //luu ten cua file.
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan cua file.
                var path = Path.Combine(Server.MapPath("~/HinhAnhSP"), fileName);
                //kiem tra hinh anh da ton tai chua.
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }
            return View();

        }
        [HttpGet]

        public ActionResult ChinhSua(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe",sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Sach sach, FormCollection f)
        {
            //Sach sach1 = db.Saches.SingleOrDefault(n => n.MaSach==sach.MaSach);
            //sach1.MoTa = sach.MoTa;
            //sach1.MoTa = f.Get("abc").ToString();
            //sach.MoTa = f["abc"].ToString();
            //db.SaveChanges();

            if (ModelState.IsValid)
            {
                //cập nhật.
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe",sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",sach.MaNXB);
            return RedirectToAction("Index");
        }
        public ActionResult HienThi(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult Xoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost,ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}