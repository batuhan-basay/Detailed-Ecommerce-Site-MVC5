using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialSoft.Models.Siniflar
{
    public class DetayAltBolum
    {
        public IEnumerable<Urun> Deger1 { get; set; }
        public IEnumerable<Detay> Deger2 { get; set; }
    }
}