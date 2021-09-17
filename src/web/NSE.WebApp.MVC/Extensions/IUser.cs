using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NSE.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid ObterUserId();
        string ObterUserEmail();
        string ObterUserToken();
        bool EstaAutenticado();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }

    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _acessor;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;

        public bool EstaAutenticado()
        {
            return _acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _acessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _acessor.HttpContext;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? _acessor.HttpContext.User.GetUserEmail() : "";
        }

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(_acessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? _acessor.HttpContext.User.GetUserToken() : "";
        }

        public bool PossuiRole(string role)
        {
            return _acessor.HttpContext.User.IsInRole(role);
        }
    }
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("sub");
            return claim?.Value;
        }
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }
        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }


    }
}
