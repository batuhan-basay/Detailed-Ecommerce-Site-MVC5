using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialSoft.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Aciklama { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        [Display(Name = "Tracking Code")]
        public string TakipKodu { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Personel { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Alici { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarih { get; set; }
    }
}