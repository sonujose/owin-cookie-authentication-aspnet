using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(OwinAuthenticationIdetity.Startup))]

namespace OwinAuthenticationIdetity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //string connectionString = ConfigurationManager.AppSettings["DefaultConnection"];
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Database=PlotoMark.AspNetIdentity";
            app.CreatePerOwinContext(() => new IdentityDbContext(connectionString));

            app.CreatePerOwinContext<UserStore<IdentityUser>>(
                (opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));

            app.CreatePerOwinContext<UserManager<IdentityUser>>(
                (opt, cont) => new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>()));

            app.CreatePerOwinContext<SignInManager<IdentityUser, string>>(
                (opt, cont) => new SignInManager<IdentityUser, string>(cont.Get<UserManager<IdentityUser>>(), cont.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });


            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
