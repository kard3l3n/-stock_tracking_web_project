using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication73.Models.Entity;
namespace WebApplication73.Controllers

{
	[Authorize]
	public class HomeController : Controller
	{
		// GET: Home
		StokEntities db = new StokEntities();
		
		public ActionResult Index()
		{
			var degerler = db.TBLURUNLER.ToList();
			return View(degerler);
		}
		[Authorize(Roles = "A")]
		[HttpGet]
		public ActionResult YeniUrun()
		{
			List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
											 select new SelectListItem
											 {
												 Text = i.KATEGORIAD,
												 Value = i.KATEGORIID.ToString()


											 }).ToList();

			ViewBag.dgr = degerler;
			return View();
		}
		[Authorize(Roles = "A")]
		[HttpPost]
		public ActionResult YeniUrun(TBLURUNLER p1)
		{
			var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
			p1.TBLKATEGORI = ktg;

			db.TBLURUNLER.Add(p1);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		[Authorize(Roles = "A")]
		public ActionResult Sil(int id)
		{
			var Delete = db.TBLURUNLER.Find(id);
			db.TBLURUNLER.Remove(Delete);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		[Authorize(Roles = "A")]
		public ActionResult UrunGetir(int id)
		{
			var urun = db.TBLURUNLER.Find(id);

			List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
											 select new SelectListItem
											 {
												 Text = i.KATEGORIAD,
												 Value = i.KATEGORIID.ToString()


											 }).ToList();

			ViewBag.dgr = degerler;

			return View("UrunGetir", urun);

		}
		[Authorize(Roles = "A")]
		public ActionResult Guncelle(TBLURUNLER p1)
		{
			var urun = db.TBLURUNLER.Find(p1.URUNID);
			urun.URUNAD = p1.URUNAD;
			var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
			urun.URUNKATEGORI = ktg.KATEGORIID;
			urun.URUNFIYAT = p1.URUNFIYAT;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult ChangeLanguage(string lang)
		{
			Session["lang"] = lang;
			return RedirectToAction("Index", "Home", new { language = lang });
		}
	}
}