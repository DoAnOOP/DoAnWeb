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
            string categoryID = Request["p-categoryID"];
            string desc = Request["p-desc"];

            //validate input   
                    try
                    {
                        //trường hợp muốn insert
                        Product prd = new Product();
                        prd.URL = URL;
                        prd.NameProduct = prdname;
                        prd.CategoryID = int.Parse(categoryID);
                        prd.isDelete = true;
                        prd.ShowOnHomePage = true;
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
                //ok
                //return "MSSV: " + mssv + "; Họ tên: " + hoten + "; Mật khẩu: " + mk;

            }
           // }


        }

        public string get_sp_Edit()
        {
            int Id;
            if (int.TryParse(Request["id"], out Id))
            {
                // Rest of your code
                //validate input
                if (Id != 0)
                {
                    try
                    {
                        //trường hợp muốn update
                        var qrs = db.Products.Where(o => o.ID == Id);
                        if (qrs.Any())
                        {
                            //có trả về bản ghi.
                            Product prd = qrs.SingleOrDefault();

                            return JsonConvert.SerializeObject(prd);
                        }
                        else
                        {
                            return "KHÔNG tìm thấy sp có ID = " + Id;
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Cập nhật thông tin SP thất bại. Chi tiết lỗi: " + ex.Message;
                    }
                }
                else
                {
                    return "Mày chơi tao không được đâu";
                }
            }
            else
            {
                return "Invalid ID";
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
    }
}