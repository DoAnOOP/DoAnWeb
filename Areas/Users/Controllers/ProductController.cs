using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Areas.Users.Controllers
{
    public class ProductController : Controller
    {
        // GET: Users/Product

        public DatabaseDataContext db = new DatabaseDataContext();

        public ActionResult Product2()
        {
            return View();
        }
        public ActionResult categorie()
        {
            return View();
        }
        public ActionResult cart()
        {
            return View();
        }
        public ActionResult checkout()
        {
            return View();
        }

        public string get_ProductsByCategory()
        {
            string category = Request["category"];
            APIResult_ett<List<Product>> rs = new APIResult_ett<List<Product>>();
            try
            {
                //truy vấn db để lấy toàn bộ dữ liệu về ds sản phẩm
                var qr = db.Products.Where(o => o.CategoryName == category);
                if (qr.Any())
                {
                    //có dữ liệu => chính là dssv
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = "Lấy DSSP thành công";
                    rs.Data = qr.ToList();
                }
                else
                {
                    //không có dữ liệu thỏa mãn
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "DSSP rỗng";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy về DSSP. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }
    }
}