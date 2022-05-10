using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineCommercialSoft.Models.Siniflar;
namespace MvcOnlineCommercialSoft.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y=>y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;
             var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamsatis = toplamurunsayisi;
            var adsoyad = c.Carilers.Where(x=> x.CariMail == mail).Select(y=> y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;


            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x=> x.Alici == mail).OrderByDescending(z => z.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelen = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.giden = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelen = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.giden = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelen = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.giden = gidensayisi;

            return View(degerler);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {

            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelen = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();

            ViewBag.giden = gidensayisi;

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            var mail = (string)Session["CariMail"];
            m.Gonderici = mail;

            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }

        public ActionResult KargoTakip(string p)
        {
            var kargolar = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            { kargolar = kargolar.Where(y => y.TakipKodu.Contains(p)); }
            return View(kargolar.ToList());
        }

        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        public PartialViewResult CariSifreDegistirme()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var caribul = c.Carilers.Find(id);

            return PartialView("CariSifreDegistirme", caribul);
        }

        public PartialViewResult CariDuyuru()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();

            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.Cariid);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}