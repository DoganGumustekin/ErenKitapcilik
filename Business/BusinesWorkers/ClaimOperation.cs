using Application.Constans;
using Castle.DynamicProxy;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Interceptors;
using System.Security.Claims;

namespace Application.BusinesWorkers
{
    public class ClaimOperation : MethodInterception
    {
        private string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value);
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; 
                }
            }
            throw new Exception(Messages.AuthorizationError);
        }
    }
}
