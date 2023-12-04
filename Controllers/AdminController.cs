
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Controllers
{
    public class AdminController : Controller
    {
        public DatabaseDataContext db = new DatabaseDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult tables()
        {
            return View();
        }
        public ActionResult productviews()
        {
            return View();
        }
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult addproducts()
        {
            return View();
        }
        public ActionResult editproducts()
        {
            return View();
        }

        public string Add()
        {
            //ví dụ về linq to sql
            //var qr = db.tbl_SVs; //select * from tbl_SV
            //var qr1 = db.tbl_SVs.Where(o => o.MSSV == "1234"); //select * from tbl_SV where mssv == "1234"

            string URL = Request["p-image"];
            string prdname = Request["p-name"];
            string price = Request["p-price"];
            string desc = Request["p-desc"];


            if (!string.IsNullOrEmpty(URL) && !string.IsNullOrEmpty(prdname) && !string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(desc))
            {
                try
                {
                    //trường hợp muốn insert
                    Product prd = new Product();
                    prd.URL = URL;
                    prd.NameProduct = prdname;
                    prd.Price = double.Parse(price);
                    prd.Description = desc;

                    db.Products.InsertOnSubmit(prd);
                    db.SubmitChanges();

                    return "Thêm mới sản phẩm thành công";
                }
                catch (Exception ex)
                {
                    return "Thêm mới sản phẩm thất bại. Chi tiết lỗi: " + ex.Message;
                }


            }
            else
            {
                return "Mày chơi tao không được đâu";
            }


        }

        public string Edit()
        {

            string id = Request["p-id"];
            string URL = Request["p-image"];
            string prdname = Request["p-name"];
            string price = Request["p-price"];
            string desc = Request["p-desc"];
            int productIntID = int.Parse(id);


            if (!string.IsNullOrEmpty(URL) && !string.IsNullOrEmpty(prdname) && !string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(desc))
            {
                try
                {
                    var qrs = db.Products.Where(o => o.ID == productIntID);
                    if (qrs.Any())
                    {
                        //có trả về bản ghi.
                        Product sp = qrs.SingleOrDefault();
                        sp.URL = URL;
                        sp.NameProduct = prdname;
                        sp.Price = double.Parse(price);
                        sp.Description = desc;

                        db.SubmitChanges();

                        return "Cập nhật thông tin sinh viên thành công";
                    }
                    else
                    {
                        return "Khong tim thay sp";
                    }
                }
                catch (Exception ex)
                {
                    return "Thêm mới sản phẩm thất bại. Chi tiết lỗi: " + ex.Message;
                }

            }
            else
            {
                return "Mày chơi tao không được đâu";
            }


        }

        public string get_All()
        {
            APIResult_ett<List<Product>> rs = new APIResult_ett<List<Product>>();
            try
            {
                //truy vấn db để lấy toàn bộ dữ liệu về ds sản phẩm
                var qr = db.Products;
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

        public string get_SP_Info()
        {
            string productID = Request["productID"];
            int productIntID = int.Parse(productID);
            try
            {
                var qr = db.Products.Where(o => o.ID == productIntID);
                if (qr.Any())
                {
                    var sp_obj = qr.SingleOrDefault();

                    return JsonConvert.SerializeObject(sp_obj);
                }
                else
                {
                    return "Không tìm thấy SP có ID=" + productID;
                }

            }
            catch (Exception ex)
            {
                return "Lấy thông tin sinh viên thất bại. Chi tiết lỗi: " + ex.Message;
            }
        }
    }
}