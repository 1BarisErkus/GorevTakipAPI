using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GorevTakipAPI.Models
{
    public class Gorev
    {
        public int ref_id { get; set; } = -1;
        public DateTime kayit_tarihi { get; set; }
        public DateTime son_degisiklik_tarihi { get; set; }
        public string degisiklik_gecmisi { get; set; } = "";
        public int kimden_id { get; set; } = -1;
        public int kime_id { get; set; } = -1;
        public string konu { get; set; } = "";
        public string aciklama { get; set; } = "";
        public string grup { get; set; } = "";
        public int durum { get; set; } = -1;
        public DateTime son_tarih { get; set; }
        public DateTime yapilma_tarihi { get; set; }
    }
}
