using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication73.Models.Entity;

namespace WebApplication73.Controllers
{
	
	public class SecurityController : Controller
    {
		// GET: Security
		
		StokEntities db = new StokEntities();
		[AllowAnonymous]

        public ActionResult Login()
        {
            return View();
        }
		[HttpPost]
		[AllowAnonymous]
		public ActionResult Login(User user)
		{
			var userdb = db.User.FirstOrDefault(x=>x.Username==user.Username && x.Password==user.Password);
			if (userdb!=null)
			{
				FormsAuthentication.SetAuthCookie(userdb.Username,false);
				return RedirectToAction("Index","Musteri");

			}
			else
			{
				ViewBag.Mesaj = "geçersiz kullanıcı adı yada şifre";
				return View();
			}
		
		}
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return View("Login");
		}


		public ActionResult ChangeLanguage(string lang)
		{
			Session["lang"] = lang;
			return RedirectToAction("Index", "Security", new { language = lang });
		}



	}
}