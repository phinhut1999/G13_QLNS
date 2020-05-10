using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class ChuDeController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: ChuDe
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(5).ToList());
        }
        public ViewResult SachTheoChuDe(int Ma=0)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe==Ma);
            if(cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> sach = db.Saches.Where(n => n.MaChuDe == Ma).OrderBy(n => n.GiaBan).ToList();
            if(sach.Count == 0)
            {
                ViewBag.Sach = "Không Có Sách Nào Thuộc Chủ Đề Này";
            }
            return View(sach);
        }
        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
    }
}