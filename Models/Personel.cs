using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GorevTakipAPI.Models
{
    public class Personel
    {
        public int ref_id { get; set; } = -1;
        public string ad { get; set; } = "";
        public string soyad { get; set; } = "";
        public string email { get; set; } = "";
        public string sifre { get; set; } = "";
        public int yeki_durum { get; set; } = -1;
    }
}
