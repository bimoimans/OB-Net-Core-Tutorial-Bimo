using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace RumahMakanPadang.Security.bll
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizedByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizedByRoleAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
