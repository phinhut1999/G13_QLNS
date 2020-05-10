using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using PagedList.Mvc;
using PagedList;

namespace mvc.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f,int ?page)
        {
            //lay tu khoa nhap vao roi tim kiem ben trong co so du lieu.
            
            string tukhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = tukhoa;
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(tukhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if(lstKQTK.Count == 0) {
                ViewBag.ThongBao = "Khong tim thay san pham nao";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Da tim thay " +lstKQTK.Count+ " San pham.";
            return View(lstKQTK.OrderBy(n=>n.TenSach).ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(string tukhoa, int? page)
        {
            //lay tu khoa nhap vao roi tim kiem ben trong co so du lieu.
            ViewBag.TuKhoa = tukhoa;
           
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(tukhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Khong tim thay san pham nao";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Da tim thay " + lstKQTK.Count + " San pham.";
            return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
    }
}