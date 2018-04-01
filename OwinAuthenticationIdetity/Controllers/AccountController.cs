using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OwinAuthenticationIdetity.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager 
            => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        public SignInManager<IdentityUser, string> SignInManager
            => HttpContext.GetOwinContext().Get<SignInManager<IdentityUser, string>>();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(RegisterModel model)
        {
            var signInStatus =  await SignInManager.PasswordSignInAsync(model.userName, model.password, true, true);

            if(signInStatus.Equals(SignInStatus.Success))
            {
                return RedirectToAction("Index", "home");
            } else
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }            
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            var identityResult = await UserManager.CreateAsync(new IdentityUser(model.userName), model.password);
            if(identityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());
            return View(model);
        }


    }

    public class RegisterModel
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}