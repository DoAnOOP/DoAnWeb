using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        DatabaseDataContext db = new DatabaseDataContext();
        // GET: Login/Login
        public ActionResult DangNhap()
        {
            return View();
        }
        public string DangNhap_action()
        {
            APIResult_ett<string> rs = new APIResult_ett<string>();

            try
            {
                //xử lý trường hợp xóa
                //lấy về mssv cần xóa
                string tk = Request["txt_tk"];
                string mk = Request["txt_mk"];

                if (!string.IsNullOrEmpty(tk) && !string.IsNullOrEmpty(mk))
                {
                    //thực hiện xóa và nên xóa mềm
                    var qr = db.Accounts.FirstOrDefault(o => o.UserName == tk && o.Password == mk);
                    if (qr != null)
                    {
                        //kiem tra admin
                        if (qr.IsAdmin == true)
                        {
                            Session["admin"] = true;
                            //trường hợp có dữ liệu                        
                            rs.ErrCode = EnumErrCode.Success;
                            rs.ErrDesc = "Đăng nhập hệ thống admin thành công";
                            rs.Data = "";
                            // Chuyển hướng đến trang admin
                            Redirect("~/Admin/addproducts");
                        }
                        else // Nếu isAdmin là 0 hoac null
                        {
                            Session["user"] = true;
                            //trường hợp có dữ liệu                        
                            rs.ErrCode = EnumErrCode.Success;
                            rs.ErrDesc = "Đăng nhập hệ thống user thành công";
                            rs.Data = "";
                            // Chuyển hướng đến trang người dùng
                            Redirect("~/Users/user/index");
                        }
                        
                    }
                    else
                    {
                        rs.ErrCode = EnumErrCode.NotExistent;
                        rs.ErrDesc = "Đăng nhập thất bại";
                        rs.Data = null;

                    }
                }
                else
                {
                    rs.ErrCode = EnumErrCode.Empty;
                    rs.ErrDesc = "Vui lòng nhập tài khoản và mật khẩu";
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = "Có lỗi xảy ra trong quá trình đăng nhập hệ thống. Chi tiết lỗi: " + ex.Message;
                rs.Data = null;
            }

            return JsonConvert.SerializeObject(rs);
        }
    }
}