
using System.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
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

        //Products
        public ActionResult productviews()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult addproducts()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult editproducts()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        //Categories
        public ActionResult addcategories()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult categoryviews()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //public ActionResult removecategory()
        //{
        //    if (System.Web.HttpContext.Current.Session["admin"] != null)
        //    {
        //        if ((bool)Session["admin"])
        //        {
        //            //trường hợp đã login rồi
        //            return View();
        //        }
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        public ActionResult editcategories()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //Accounts
        public ActionResult accountviews()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult editaccounts()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        public ActionResult addaccounts()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                if ((bool)Session["admin"])
                {
                    //trường hợp đã login rồi
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        //ACCOUNTS
        public bool IsAccountNameExists(string accountNameToCompare)
        {
            bool usernames = db.Accounts.Any(o => o.UserName == accountNameToCompare);
            return usernames;
        }

        public string Add_acc()
        {
            string ID = Request["a-id"];
            string Username = Request["a-name"];
            Boolean Roles = Convert.ToBoolean(Request["a-roles"]);
            Boolean IsExist = IsAccountNameExists(Username);
            if (!string.IsNullOrEmpty(Username))
            {
                if (IsExist)
                {
                    return "Tài khoản đã tồn tại";
                }
                else
                {
                    try
                    {
                        Account acc = new Account();
                        acc.UserName = Username;
                        acc.IsAdmin = Roles;

                        db.Accounts.InsertOnSubmit(acc);
                        db.SubmitChanges();

                        return "Thêm mới tài khoản thành công";
                    }
                    catch (Exception ex)
                    {
                        return "Thêm mới tài khoản thất bại. Chi tiết lỗi: " + ex.Message;
                    }
                }
            }
            else
            {
                return "Vui lòng điền đầy đủ thông tin";
            }
        }
        public string get_All_acc()
        {
            APIResult_ett<List<Account>> rs = new APIResult_ett<List<Account>>();
            try
            {
                var qr = db.Accounts;
                if (qr.Any())
                {
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = "Lấy danh sách tài khoản thành công";
                    rs.Data = qr.ToList();
                }
                else
                {
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Danh sách tài khoản rỗng";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy danh sách tài khoản. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }
            return JsonConvert.SerializeObject(rs);
        }
        //Sua quyen tai khoan
        public string Edit_acc()
        {
            Boolean Roles = Convert.ToBoolean(Request["a-roles"]);
            string Username = Request["a-name"];
            {
                try
                {
                    var qrs = db.Accounts;
                    if (qrs.Any())
                    {
                        Account acc = qrs.FirstOrDefault();
                        acc.IsAdmin = Roles;
                        //acc.UserName = Username;
                        db.SubmitChanges();
                        return "Cập nhật thông tin tài khoản thành công";
                    }
                }
                catch (Exception ex)
                {
                    return "Cập nhật thông tin tài khoản thất bại. Chi tiết lỗi: " + ex.Message;
                }
                return "Lỗi";
            }
        }
        public string get_Category()
        {
            APIResult_ett<List<Category>> rs = new APIResult_ett<List<Category>>();
            try
            {
                //truy vấn db để lấy toàn bộ dữ liệu về ds sản phẩm
                var qr = db.Categories;
                if (qr.Any())
                {
                    //có dữ liệu => chính là dssv
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = "Lấy DS Category thành công";
                    rs.Data = qr.ToList();
                }
                else
                {
                    //không có dữ liệu thỏa mãn
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "DS Category rỗng";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy về DS Category. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }
        public string Add_cate()
        {
            string id = Request["c-id"];
            string catename = Request["c-name"];
            DateTime NOW = DateTime.Now;
            Boolean IsExist = IsCategoryNameExists(catename);
            if (!string.IsNullOrEmpty(catename))
            {
                if (IsExist)
                {
                    return "Danh mục đã tồn tại";
                }
                else
                {
                    try
                    {
                        Category cate = new Category();
                        cate.NameCategory = catename;
                        cate.CreatedOnUtc = NOW;

                        db.Categories.InsertOnSubmit(cate);
                        db.SubmitChanges();

                        return "Thêm mới danh mục thành công";
                    }
                    catch (Exception ex)
                    {
                        return "Thêm mới danh mục thất bại. Chi tiết lỗi: " + ex.Message;
                    }
                }
            }
            else
            {
                return "Xin Nhập Tên Mục Sản Phẩm Muốn Thêm";
            }
        }
        public string Edit_cate()
        {
            string id = Request["c-id"];
            string catename = Request["c-name"];
            DateTime NOW = DateTime.Now;
            Boolean IsExist = IsCategoryNameExists(catename);
            //int CategoryIntID = int.Parse(id);
            if (!string.IsNullOrEmpty(catename))
            {
                if (IsExist)
                {
                    return "Danh mục đã tồn tại";
                }
                else
                {
                    try
                    {
                        var qrs = db.Categories.Where(o => o.ID == int.Parse(id));
                        if (qrs.Any())
                        {
                            Category cate = qrs.SingleOrDefault();
                            cate.NameCategory = catename;
                            cate.UpdatedOnUtc = NOW;

                            db.SubmitChanges();
                            return "Cập nhật thông tin danh mục thành công";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Cập nhật thông tin danh mục thất bại. Chi tiết lỗi: " + ex.Message;
                    }
                }
            }
            else
            {
                return "Bạn đã để trống trường dữ liệu";
            }
            return "Lỗi";
        }
        public string Search_acc()
        {
            APIResult_ett<List<Account>> rs = new APIResult_ett<List<Account>>();
            try
            {
                string search_val = Request["search_val"];
                //string search_type = Request["search_type"];

                if (!string.IsNullOrEmpty(search_val))
                {
                    //truy vấn db để lấy toàn bộ dữ liệu về ds sinh viên
                    IQueryable<Account> qr = null;
                    qr = db.Accounts.Where(o => o.UserName.Contains(search_val));
                    if (qr.Any())
                    {
                        //có dữ liệu => chính là dssv
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Tìm kiếm tài khoản thành công";
                        rs.Data = qr.ToList();
                    }
                    else
                    {
                        //không có dữ liệu thỏa mãn
                        rs.ErrCode = EnumErrCode.Empty;
                        rs.ErrDesc = "Không tìm thấy tài khoản thỏa mãn điều kiện tìm kiếm";
                        rs.Data = null;
                    }
                }
                else
                {
                    //get all
                    var qr = db.Accounts;
                    if (qr.Any())
                    {
                        //có dữ liệu => chính là dssv
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Lấy tài khoản thành công";
                        rs.Data = qr.ToList();
                    }
                    else
                    {
                        //không có dữ liệu thỏa mãn
                        rs.ErrCode = EnumErrCode.Empty;
                        rs.ErrDesc = "Danh sách tài khoản rỗng";
                        rs.Data = null;
                    }

                    //rs.ErrCode = EnumErrCode.InputEmpty;
                    //rs.ErrDesc = "Vui lòng nhập đầy đủ giá trị và tiêu chí cần tìm kiếm";
                    //rs.Data = null;
                }

            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy về DSTK. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }

        public string Del_Cate()
        {
            APIResult_ett<string> rs = new APIResult_ett<string>();

            try
            {
                //xử lý trường hợp xóa
                //lấy về mssv cần xóa
                string cateId = Request["cateId"];
                if (!string.IsNullOrEmpty(cateId))
                {
                    //thực hiện xóa và nên xóa mềm
                    var qr = db.Categories.Where(o => o.ID == int.Parse(cateId));
                    if (qr.Any())
                    {
                        //trường hợp có dữ liệu
                        Category del_obj = qr.SingleOrDefault();
                        del_obj.isDelete = true;

                        db.SubmitChanges();

                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Xóa danh mục có mã " + cateId + " thành công";
                        rs.Data = del_obj.NameCategory;
                    }
                    else
                    {
                        rs.ErrCode = EnumErrCode.NotExistent;
                        rs.ErrDesc = "Xóa danh mục có mã " + cateId + " thất bại do không tìm thấy";
                        rs.Data = null;

                    }
                }
                else
                {
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Vui lòng nhập mã cần xóa";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Xóa danh mục thất bại. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }
        public string Search_Cate()
        {
            APIResult_ett<List<Category>> rs = new APIResult_ett<List<Category>>();
            try
            {
                string search_val = Request["search_val"];
                string search_type = Request["search_type"];

                if (!string.IsNullOrEmpty(search_val) && !string.IsNullOrEmpty(search_type))
                {
                    //truy vấn db để lấy toàn bộ dữ liệu về ds sinh viên
                    IQueryable<Category> qr = null;

                    switch (search_type)
                    {
                        case "ID":
                            qr = db.Categories.Where(o => o.ID == int.Parse(search_val) && (o.isDelete == null || o.isDelete == false));
                            break;
                        case "NameCate":
                            qr = db.Categories.Where(o => o.NameCategory.Contains(search_val) && (o.isDelete == null || o.isDelete == false));
                            break;
                        default:
                            break;
                    }

                    if (qr.Any())
                    {
                        //có dữ liệu => chính là dssv
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Tìm kiếm danh mục thành công";
                        rs.Data = qr.ToList();
                    }
                    else
                    {
                        //không có dữ liệu thỏa mãn
                        rs.ErrCode = EnumErrCode.Empty;
                        rs.ErrDesc = "Không tìm thấy danh mục thỏa mãn điều kiện tìm kiếm";
                        rs.Data = null;
                    }
                }
                else
                {
                    //get all
                    var qr = db.Categories.Where(o => o.isDelete == null || o.isDelete == false);
                    if (qr.Any())
                    {
                        //có dữ liệu => chính là dssv
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Lấy danh mục thành công";
                        rs.Data = qr.ToList();
                    }
                    else
                    {
                        //không có dữ liệu thỏa mãn
                        rs.ErrCode = EnumErrCode.Empty;
                        rs.ErrDesc = "Danh sách danh mục rỗng";
                        rs.Data = null;
                    }

                    //rs.ErrCode = EnumErrCode.InputEmpty;
                    //rs.ErrDesc = "Vui lòng nhập đầy đủ giá trị và tiêu chí cần tìm kiếm";
                    //rs.Data = null;
                }

            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy về Danh sách danh mục. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }

        //CHECK IF CATEGORY NAME ALREADY EXISTS
        public bool IsCategoryNameExists(string categoryNameToCompare)
        {
            bool categoryNames = db.Categories.Any(o => o.NameCategory == categoryNameToCompare);
            return categoryNames;
        }
        //VIEW ALL CATEGORIES
        public string get_All_cate()
        {
            APIResult_ett<List<Category>> rs = new APIResult_ett<List<Category>>();
            try
            {
                var qr = db.Categories.Where(o => o.isDelete == false || o.isDelete == null );
                if (qr.Any())
                {
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = "Lấy danh mục thành công";
                    rs.Data = qr.ToList();
                }
                else
                {
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Danh mục rỗng";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình lấy danh mục. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }
      
        public string get_Cate_Info()
        {
            string CateID = Request["catergoryID"];
            //int productIntID = int.Parse(productID);
            try
            {
                var qr = db.Categories.Where(o => o.ID == int.Parse(CateID));
                if (qr.Any())
                {
                    var sp_obj = qr.SingleOrDefault();

                    return JsonConvert.SerializeObject(sp_obj);
                }
                else
                {
                    return "Không tìm thấy Cate có ID=" + CateID;
                }

            }
            catch (Exception ex)
            {
                return "Lấy thông tin sản phẩm thất bại. Chi tiết lỗi: " + ex.Message;
            }
        }
        //PRODUCTS
        public string Add_prd()
        {
            string URL = Request["p-image"];
            string prdname = Request["p-name"];
            string price = Request["p-price"];
            string desc = Request["p-desc"];
            string category = Request["p-category"];

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
                    prd.CategoryName = category;
                    prd.CreatedOnUtc = DateTime.Now;

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
                return "Bạn Chưa Nhập Đủ Dữ Liệu";
            }
        }
        public string Edit_prd()
        {
            string id = Request["p-id"];
            string URL = Request["p-image"];
            string prdname = Request["p-name"];
            string price = Request["p-price"];
            string desc = Request["p-desc"];
            string category = Request["p-category"];
          //  int productIntID = int.Parse(id);

            if (!string.IsNullOrEmpty(URL) && !string.IsNullOrEmpty(prdname) && !string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(desc))
            {
                try
                {
                    var qrs = db.Products.Where(o => o.ID == int.Parse(id));
                    if (qrs.Any())
                    {
                        //có trả về bản ghi.
                        Product sp = qrs.SingleOrDefault();
                        sp.URL = URL;
                        sp.NameProduct = prdname;
                        sp.Price = double.Parse(price);
                        sp.Description = desc;
                        sp.CategoryName = category;
                        sp.UpdatedOnUtc = DateTime.Now;

                        db.SubmitChanges();

                        return "Cập nhật thông tin sản phẩm thành công";
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
                return "Bạn đã để trống một số dữ liệu";
            }
        }
        public string Del_prd()
        {
            APIResult_ett<string> rs = new APIResult_ett<string>();

            try
            {
                //xử lý trường hợp xóa
                //lấy về mssv cần xóa
                string prdID = Request["prdID"];
                if (!string.IsNullOrEmpty(prdID))
                {
                    //thực hiện xóa và nên xóa mềm
                    var qr = db.Products.Where(o => o.ID == int.Parse(prdID));
                    if (qr.Any())
                    {
                        //trường hợp có dữ liệu
                        Product del_obj = qr.SingleOrDefault();
                        del_obj.isDelete = true;

                        db.SubmitChanges();

                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = "Xóa SP có MSP " + prdID + " thành công";
                        rs.Data = del_obj.CategoryName;
                    }
                    else
                    {
                        rs.ErrCode = EnumErrCode.NotExistent;
                        rs.ErrDesc = "Xóa SP có MSP " + prdID + " thất bại do không tìm thấy";
                        rs.Data = null;

                    }
                }
                else
                {
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Vui lòng nhập MSP cần xóa";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Xóa SP thất bại. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }

        public string get_All()
        {
            APIResult_ett<List<Product>> rs = new APIResult_ett<List<Product>>();
            try
            {
                //truy vấn db để lấy toàn bộ dữ liệu về ds sản phẩm
                var qr = db.Products.Where(o => o.isDelete == false || o.isDelete == null);
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
            //int productIntID = int.Parse(productID);
            try
            {
                var qr = db.Products.Where(o => o.ID == int.Parse(productID));
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
                return "Lấy thông tin sản phẩm thất bại. Chi tiết lỗi: " + ex.Message;
            }
        }
    }
}