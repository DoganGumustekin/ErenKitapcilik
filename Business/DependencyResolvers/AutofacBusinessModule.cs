using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Module = Autofac.Module;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Interceptors;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookService>().As<IBookService>().SingleInstance();
            builder.RegisterType<BookDal>().As<IBookDal>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserDal>().As<IUserDal>();

            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); 

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() 
                }).SingleInstance();
        }
    }
}
