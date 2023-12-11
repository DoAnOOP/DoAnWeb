using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test1.Areas.Users.Controllers
{
    public class ProductController : Controller
    {
        // GET: Users/Product
        public ActionResult Product2()
        {
            if (System.Web.HttpContext.Current.Session["user"] != null)
            {
                if ((bool)Session["user"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return Redirect("~/TaiKhoan/DangNhap"); 
        }
        public ActionResult categorie()
        {
            if (System.Web.HttpContext.Current.Session["user"] != null)
            {
                if ((bool)Session["user"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return Redirect("~/TaiKhoan/DangNhap");
        }
        public ActionResult cart()
        {
            if (System.Web.HttpContext.Current.Session["user"] != null)
            {
                if ((bool)Session["user"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return Redirect("~/TaiKhoan/DangNhap");
        }
        public ActionResult checkout()
        {
            if (System.Web.HttpContext.Current.Session["user"] != null)
            {
                if ((bool)Session["user"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return Redirect("~/TaiKhoan/DangNhap");
        }
    }
}