using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace HotSpringProject.Filter
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string username = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string requestedAction = filterContext.ActionDescriptor.ActionName;
            string requestedPage = $"/{username}/{requestedAction}";
            EmployEmp employEmp = (EmployEmp)HttpContext.Current.Session["User"];
            if (requestedPage == "/Default/Login" || requestedPage == "/Default/Loginverify"|| requestedPage == "/EquUpkeepTask/upkeeptask")
            {
                return;
            }
            else if (employEmp == null)
            {
                // 如果会话为空或为新会话，则重定向到登录页面
                filterContext.Result = new RedirectResult("/Default/Login");
            }
            else
            {
                string name = employEmp.name;

                var data = HttpRuntime.Cache.Get(name) as UserData;
                if (data == null)
                {
                    // 如果缓存中没有 UserData，则重定向到登录页面
                    filterContext.Result = new RedirectResult("/Default/Login");
                }
                else
                {
                    // 验证请求的页面地址是否在缓存中
                    var pageStatus = data.PageStatus.FirstOrDefault(p => p.pageAddress.Equals(requestedPage, StringComparison.OrdinalIgnoreCase));

                    if (pageStatus == default)
                    {
                        // 如果请求的页面地址不在缓存中，则放行，不拦截
                        return;
                    }
                    else
                    {
                        // 如果请求的页面地址在缓存中，再次验证页面状态
                        if (pageStatus.status == 1)
                        {
                            // 如果页面状态为 1，则放行，不拦截
                            return;
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult("/Default/Login");
                        }
                    }
                }
            }

        }

    }
}