using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MvcOnlineCommercialSoft.Models.Siniflar;

namespace MvcOnlineCommercialSoft.Controllers
{
    public class GrafikController : Controller
    {
        Context c = new Context();
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var drawchart = new Chart(600, 600);
            drawchart.AddTitle("Kategori - Ürün stok sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Fridge", "Small Appliances", "Computer", "Phone", "Furniture", "Decoration" }, yValues: new[] { 85, 65, 98, 75, 65, 10 }).Write();
            return File(drawchart.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var result = c.Uruns.ToList();
            result.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            result.ToList().ForEach(y => yvalue.Add(y.Stok));
            var chart = new Chart(width: 800, height: 800).AddTitle("Stoklar").AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(chart.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(),JsonRequestBehavior.AllowGet);
        }

        public List<GoogleChart> Urunlistesi()
        {
            List<GoogleChart> chart = new List<GoogleChart>();

            chart.Add(new GoogleChart()
            {
                urunad = "Bilgisayar",
                stok = 120
            });

            chart.Add(new GoogleChart()
            {
                urunad = "Beyaz Eşya",
                stok = 250
            });
            chart.Add(new GoogleChart()
            {
                urunad = "Mobilya",
                stok = 50
            });
            chart.Add(new GoogleChart()
            {
                urunad = "Küçük Ev aletleri",
                stok = 100
            });
            chart.Add(new GoogleChart()
            {
                urunad = "Telefonlar",
                stok = 500
            });

            return chart;
        }

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(Urunlistesi2(), JsonRequestBehavior.AllowGet);
        }


        public List<GoogleChart2> Urunlistesi2()
        {
            List<GoogleChart2> chart = new List<GoogleChart2>();
            using (var c = new Context())
            {
                chart = c.Uruns.Select(x => new GoogleChart2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }


                return chart;
        }
    }
}