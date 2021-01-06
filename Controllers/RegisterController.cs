using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication73.Models.Entity;
namespace WebApplication73.Controllers
{
    public class RegisterController : Controller
    {
		StokEntities db = new StokEntities();

		[Authorize(Roles = "A")]
		// GET: Register
		public ActionResult Index()
        {
			var degerler = db.User.ToList();
			return View(degerler);
        }

		[HttpGet]
		public ActionResult YeniKisi()
		{
			return View();
		}

		[HttpPost]
		public ActionResult YeniKisi(User p1)
		{
			db.User.Add(p1);
			db.SaveChanges();
			return View();
		}

		public ActionResult Sil(int id)
		{
			var Delete = db.User.Find(id);
			db.User.Remove(Delete);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KisiGetir(int id)
		{
			var Kisi = db.User.Find(id);
			return View("KisiGetir", Kisi);

		}


		public ActionResult Guncelle(User p1)
		{
			var Kisi = db.User.Find(p1.UserId);
			Kisi.Username = p1.Username;
			Kisi.Password = p1.Password;
			Kisi.Role = p1.Role;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult ChangeLanguage(string lang)
		{
			Session["lang"] = lang;
			return RedirectToAction("Index", "Register", new { language = lang });
		}



	}
}