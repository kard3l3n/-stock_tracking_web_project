using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication73.Models.Entity;
namespace WebApplication73.Controllers
{
	[Authorize]
	
	public class KategoriController : Controller
	{
		// GET: Home
		StokEntities db = new StokEntities();
		
		public ActionResult Index()
		{
			var degerler = db.TBLKATEGORI.ToList();
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
		public ActionResult YeniUrun(TBLKATEGORI p1)
		{
			db.TBLKATEGORI.Add(p1);
			db.SaveChanges();
			return View();
		}

		[Authorize(Roles = "A")]
		public ActionResult Sil(int id)
		{
			var Delete = db.TBLKATEGORI.Find(id);
			db.TBLKATEGORI.Remove(Delete);
			db.SaveChanges();


			return RedirectToAction("Index");
		}

		[Authorize(Roles = "A")]
		public ActionResult KategoriGetir(int id)
		{
			var ktgr = db.TBLKATEGORI.Find(id);
			return View("KategoriGetir",ktgr);

		}

		
		public ActionResult Guncelle(TBLKATEGORI p1)
		{
			var ktg = db.TBLKATEGORI.Find(p1.KATEGORIID);
			ktg.KATEGORIAD = p1.KATEGORIAD;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult ChangeLanguage(string lang)
		{
			Session["lang"] = lang;
			return RedirectToAction("Index", "Kategori", new { language = lang });
		}
	}
}