using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineCommercialSoft.Models.Siniflar;

namespace MvcOnlineCommercialSoft.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var degerler = c.Perosnels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dprtmn = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {

            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName); /*Dosya uzanstısı alınır*/
                string href = "~/Image/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(href));
                p.PersonelGorsel = "/Image/" + filename + extension;
            }

            c.Perosnels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dprtmn = deger1;
            var personel = c.Perosnels.Find(id);
            return View("PersonelGetir", personel);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName); /*Dosya uzanstısı alınır*/
                string href = "~/Image/" + filename + extension;
                Request.Files[0].SaveAs(Server.MapPath(href));
                p.PersonelGorsel = "/Image/" + filename + extension;
            }
            var personel = c.Perosnels.Find(p.Personelid);
            personel.PersonelAd = p.PersonelAd;
            personel.PersonelSoyad = p.PersonelSoyad;
            personel.PersonelGorsel = p.PersonelGorsel;
            personel.Departmanid = p.Departmanid;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelList()
        {
            var sorgu = c.Perosnels.ToList();
            return View(sorgu);
        }
    }
}