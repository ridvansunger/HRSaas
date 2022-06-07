

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace HRSaas.Core.UI.Authorize
{
    public class CustomAuthorize : AuthorizeAttribute,IAsyncAuthorizationFilter
    {
        private readonly string _filterPArameter;

        public CustomAuthorize(string filterPArameter)
        {
            _filterPArameter = filterPArameter;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("www.google.com");
            }

        }
    }
}
