using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Estudos.App.Web.Extensions
{
    public class CustomRoleAuthorization
    {
        public static bool ValidarRoleUsuario(HttpContext context, string[] roles)
        {
            var autorizado = false;

            foreach (var role in roles)
            {
                if (context.User.IsInRole(role))
                    autorizado = true;
            }
            return autorizado;
        }

    }

    public class RoleAuthorizeAttribute : TypeFilterAttribute
    {
        public RoleAuthorizeAttribute(string[] roles) : base(typeof(RequisitoRoleFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class RequisitoRoleFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;

        public RequisitoRoleFilter(IReadOnlyList<object> arguments)
        {
            _roles = (string[])arguments;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }

            if (!CustomRoleAuthorization.ValidarRoleUsuario(context.HttpContext, _roles))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}