    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test1.Shareds
{
    public class Utils
    {
        public static bool check_login(HttpSessionStateBase Session)
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null && System.Web.HttpContext.Current.Session["user"] != null)
            {
                if ((bool)Session["admin"] || (bool)Session["user"])
                {
                    //trường hợp đã login rồi
                    return true;
                }
            }
            return false;
        }
    }
}