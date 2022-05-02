using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialSoft.Models.Siniflar
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Baslik { get; set; }
        [Column(TypeName = "bit")]
        public bool Durum { get; set; }
    }
}