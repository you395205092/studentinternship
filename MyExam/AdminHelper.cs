using Exam.Common;
using Exam.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExam
{
    public static class AdminHelper
    {
        public static AdminDTO GetAdminId(HttpContextBase ctx)
        {
            var data = JsonConvert.DeserializeObject<AdminDTO>(DESEncrypt.Decrypt(CommonHelper.GetCookie("AdminLogin")));
            return data;
        }

        public static UserDTO GetUserId(HttpContextBase ctx)
        {

            var data = JsonConvert.DeserializeObject<UserDTO>(DESEncrypt.Decrypt(CommonHelper.GetCookie("UserLogin")));
            return data;
        }
    }
}