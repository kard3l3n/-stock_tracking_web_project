using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication73.Models.Entity;
namespace WebApplication73.Controllers
{
	[Authorize]
	public class MusteriController : Controller
	{
		// GET: Home
		StokEntities db = new StokEntities();
		
		public ActionResult Index()
		{
			var degerler = db.TBLMUSTERI.ToList();
			return View(degerler);
		}

		[Authorize(Roles = "A")]
		[HttpGet]
		public ActionResult YeniUrun()
		{
			return View();
		}

		[Authorize(Roles = "A")]
		[HttpPost]
		public ActionResult YeniUrun(TBLMUSTERI p1)
		{
			db.TBLMUSTERI.Add(p1);
			db.SaveChanges();
			return View();
		}

		[Authorize(Roles = "A")]
		public ActionResult Sil(int id)
		{
			var Delete = db.TBLMUSTERI.Find(id);
			db.TBLMUSTERI.Remove(Delete);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		[Authorize(Roles = "A")]
		public ActionResult MusteriGetir(int id)
		{
			var mus = db.TBLMUSTERI.Find(id);
			return View("MusteriGetir",mus);
		}


		public ActionResult Guncelle(TBLMUSTERI p1)
		{
			var musteri = db.TBLMUSTERI.Find(p1.MUSTERIID);
			musteri.MUSTERIAD = p1.MUSTERIAD;
			musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult ChangeLanguage(string lang)
		{
			Session["lang"] = lang;
			return RedirectToAction("Index", "Musteri", new { language = lang });
		}
	}
}