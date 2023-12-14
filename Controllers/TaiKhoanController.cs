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
        public DatabaseDataContext db = new DatabaseDataContext();
        // GET: TaiKhoan
        // GET: Login/Login
        public ActionResult DangNhap()
        {
            return View();
        }
        public string DangNhap_action()
        {
            APIResult_ett<List<Account>> rs = new APIResult_ett<List<Account>>();

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
                            rs.Data = db.Accounts.Where(o => o.UserName == tk && o.Password == mk).ToList();
                            // Chuyển hướng đến trang admin
                            
                        }
                        else // Nếu isAdmin là 0 hoac null
                        {
                            Session["user"] = true;
                            //trường hợp có dữ liệu                        
                            rs.ErrCode = EnumErrCode.Success;
                            rs.ErrDesc = "Đăng nhập hệ thống user thành công";
                            rs.Data = db.Accounts.Where(o => o.UserName == tk && o.Password == mk).ToList();
                            // Chuyển hướng đến trang người dùng
                            
                        }
                        
                    }
                    else
                    {
                        rs.ErrCode = EnumErrCode.NotExistent;
                        rs.ErrDesc = "Tài Khoản Hoặc Mật Khẩu Không Đúng Vui Lòng Nhập Lại";
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

            return JsonConvert.SerializeObject(rs)
           ;
        }
        public string DangKy_action()
        {
            string tkdk = Request["txt_tkdk"];
            string mkdk = Request["txt_mkdk"];
            string email = Request["txt_email"];

            if (!string.IsNullOrEmpty(tkdk) && !string.IsNullOrEmpty(mkdk) && !string.IsNullOrEmpty(email))
            {
                try
                {
                    //trường hợp muốn insert
                    Account taikhoan = new Account();
                    taikhoan.UserName = tkdk;
                    taikhoan.Password = mkdk;
                    taikhoan.Email = email;

                    db.Accounts.InsertOnSubmit(taikhoan);
                    db.SubmitChanges();

                    return "Tạo tài khoản thành công";
                }
                catch (Exception ex)
                {
                    return "Tạo tài khoản thất bại. Chi tiết lỗi: " + ex.Message;
                }
            }
            else
            {
                return "Vui Lòng Nhập Vào Dữ Liệu Cần Thiết";
            }
        }
    }
}