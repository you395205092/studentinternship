using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExam.App_Start
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckPermissionAttribute : Attribute
    {
        public string Permission { get; set; }
        public CheckPermissionAttribute(string permission)
        {
            this.Permission = permission;
        }
    }
}