using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Infrastructure;

namespace XeKaDo.CrossCutting.Extensions
{
    public static class UserExtensions
    {
        public static UserInfo User(this ClaimsPrincipal user)
        {
            if(!user.Identity.IsAuthenticated) { return new UserInfo(); }
            return new UserInfo()
            {
                Id = user.GetId(),
                Nome = user.GetName(),
                Email = user.GetEmail()
            };
        }

        public static string GetId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated) { return string.Empty; }
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            if(!user.Identity.IsAuthenticated) { return string.Empty; }
            return user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            if(!user.Identity.IsAuthenticated) { return string.Empty; }
            return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }
    }
}
