using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Exam.Common
{
    public class CommonHelper
    {

        public static string JumpAlertState(int state)
        {

            string url = "";
            string txt = "";
            if (state == 2)
            {
                txt = "您已经报名了项目，可以开始测试！";
                url = "/Home/PaperList";
            }
            else if (state == 3)
            {
                txt = "您已经完成测试，等待老师分配岗位！";
                url = "/Home/WorkPost";
            }
            else if (state == 4)
            {
                txt = "老师已分配岗位，请选择岗位！";
                url = "/Home/WorkPost";
            }
            else if (state == 5)
            {
                txt = "您已经选择岗位，请上传签证和个人简介！";
                url = "/Home/UserDataMore";
            }
            else if (state == 6)
            {
                txt = "您已经选择签证，请下载DS表！";
                url = "/Home/IsUsa";
            }
            else if (state == 7)
            {
                txt = "您已经下载DS表,请确认是否抵达美国！";
                url = "/Home/IsUsa";
            }
            else if (state == 8)
            {
                txt = "您已经抵达美国，如有问题请提交留言！";
                url = "/Home/MessageList";
            }
            else if (state == 9)
            {
                txt = "您已经回国！";
                url = "/Home/BackHome";
            }
            else
            {
                txt = "欢迎您的到来，请报名！";
                url = "/Home/Project";
            }

            string str = string.Format("<script>alert('{0}');window.location.href='{1}'</script>",txt,url);


            return str;
        }

        public static string JumpAlert(string txt, string url)
        {
            string str = string.Format("<script>alert('{0}');window.location.href='{1}'</script>", txt, url);
            return str;
        }

        public static string StateUrl(int state)
        {
            string url = "";
             if (state==2)
            {
                url = "/Home/PaperList";
            }
            else if (state==3)
            {
                url = "/Home/WorkPost";
            }
            else if (state == 4)
            {
                url = "/Home/UserDataMore";
            }
            else if (state == 5)
            {
                url = "/Home/IsUsa";
            }
            else if (state == 6)
            {
                url = "/Home/Message";
            }
            else
            {
                url = "/Home/Project";
            }
            return url;
        }

        public static string CalcMD5(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }

        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        //Chapcha
        public static string CreateVerifyCode(int len)
        {
            char[] data = { 'a','c','d','e','f','h','k','m',
                'n','r','s','t','w','x','y'};
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = rand.Next(data.Length);//[0,data.length)
                char ch = data[index];
                sb.Append(ch);
            }
            //勤测！
            return sb.ToString();
        }


        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetIP()
        {
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }



        #region Cookie操作
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }
        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public static void RemoveCookie(string CookiesName)
        {
            HttpCookie objCookie = new HttpCookie(CookiesName.Trim());
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        #endregion
    }
}
