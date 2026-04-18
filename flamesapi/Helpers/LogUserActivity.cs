using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flamesapi.Extensions;
using flamesapi.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace flamesapi.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userid = resultContext.HttpContext.User.GetUserId();
            if (userid == 0) return;

            var userRepository = resultContext.HttpContext.RequestServices.GetService(typeof(IUserRepository)) as IUserRepository;
            if (userRepository == null) return;

            var user = await userRepository.GetUserByIdAsync(userid);
            if (user == null) return;
            user.LastActive = DateTime.Now;
            await userRepository.SaveAllAsync();
        }
    }
}