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
            if (System.Web.HttpContext.Current.Session["is_login"] != null)
            {
                if ((bool)Session["is_login"])
                {
                    //trường hợp đã login rồi
                    return true;
                }
            }
            return false;
        }
    }
}