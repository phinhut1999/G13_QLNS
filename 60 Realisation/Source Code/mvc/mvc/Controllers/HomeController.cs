using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using PagedList;
using PagedList.Mvc;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: Home
        public ActionResult Index(int? page)
        {

            //tao bien so san pham tren 1 trang,  
            int pageSize = 9;
              //taobien so trang.
            var pageNumber = (page ?? 1);
            return View(db.Saches.Where(n=>n.Moi==1).OrderBy(n=>n.TenSach).ToPagedList(pageNumber,pageSize));
        }
    }
}