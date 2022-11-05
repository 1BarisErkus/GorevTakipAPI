using Dapper;
using GorevTakipAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GorevTakipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GorevController : Controller
    {

        // http://localhost:50396/Gorev/liste

        string conString = "DATA SOURCE = BARIS; INITIAL CATALOG = GorevTakipDB; USER ID = sa; PASSWORD = 123; Trusted_Connection = true;";

        [HttpGet("liste")]
        public IActionResult Listele(int id)
        {
            string sorgu = "";

            List<Gorev> GorevList = new List<Gorev>();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                if (id > 0)
                    sorgu = "SELECT * FROM tbl_gorev WHERE ref_id =" + id;
                else
                    sorgu = "SELECT * FROM tbl_gorev";

                GorevList = conn.Query<Gorev>(sorgu).ToList();
            }

            return Ok(GorevList);
        }

        [HttpGet("ara")]
        public IActionResult Ara(string gelenKonu)
        {
            string sorgu = "";

            List<Gorev> GorevList = new List<Gorev>();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                sorgu = "SELECT * FROM tbl_gorev WHERE konu like '%" + gelenKonu + "%'";

                GorevList = conn.Query<Gorev>(sorgu).ToList();
            }

            return Ok(GorevList);
        }

        [HttpDelete("{id}")]
        public IActionResult Sil(int id)
        {

            using (SqlConnection conn = new SqlConnection(conString))
            {
                string sorgu = "DELETE FROM tbl_gorev WHERE ref_id =" + id;
                conn.Execute(sorgu);
            }

            return Ok();
        }

        private void GorevGecerlilikKontrolu(Gorev gorev)
        {
            if (string.IsNullOrEmpty(gorev.konu)) throw new Exception("Konu boş olamaz.");
            gorev.son_degisiklik_tarihi = DateTime.Now;
        }

        [HttpPost]
        public async Task<ActionResult<Gorev>> Ekle(Gorev gorev)
        {
            //try
            //{
            //    GorevGecerlilikKontrolu(gorev);
            using (SqlConnection conn = new SqlConnection(conString))
            {
                //gorev.son_tarih = DateTime.Now;
                //gorev.yapilma_tarihi = DateTime.Now;

                string sorgu = "INSERT INTO tbl_gorev VALUES(@kayit_tarihi, @son_degisiklik_tarihi, @degisiklik_gecmisi," +
                    " @kimden_id, @kime_id, @konu, @aciklama, @grup, @durum, @son_tarih, @yapilma_tarihi)";
                var prms = new
                {
                    kayit_tarihi = DateTime.Now,
                    son_degisiklik_tarihi = DateTime.Now,
                    degisiklik_gecmisi = gorev.degisiklik_gecmisi,
                    kimden_id = gorev.kimden_id,
                    kime_id = gorev.kime_id,
                    konu = gorev.konu,
                    aciklama = gorev.aciklama,
                    grup = gorev.grup,
                    durum = gorev.durum,
                    son_tarih = gorev.son_tarih,
                    yapilma_tarihi = gorev.yapilma_tarihi
                };

                conn.Execute(sorgu, prms);
            }
            return Ok();
            //}
            //catch (Exception ex1)
            //{
            //    return BadRequest("API HATASI: " + ex1.Message);
            //}
        }

        [HttpPut]
        public async Task<ActionResult<Gorev>> Guncelle(Gorev gorev)
        {
            //try
            //{
            //    GorevGecerlilikKontrolu(gorev);
            using (SqlConnection conn = new SqlConnection(conString))
            {
                //gorev.son_tarih = DateTime.Now;
                //gorev.yapilma_tarihi = DateTime.Now;

                string sorgu = "UPDATE tbl_gorev SET son_degisiklik_tarihi = @son_degisiklik_tarihi, " +
                    "degisiklik_gecmisi = @degisiklik_gecmisi, kimden_id = @kimden_id, kime_id = @kime_id, konu = @konu, " +
                    "aciklama = @aciklama, grup = @grup, durum = @durum, son_tarih = @son_tarih, yapilma_tarihi = @yapilma_tarihi WHERE ref_id = @id";
                var prms = new
                {
                    id = gorev.ref_id,
                    son_degisiklik_tarihi = DateTime.Now,
                    degisiklik_gecmisi = gorev.degisiklik_gecmisi,
                    kimden_id = gorev.kimden_id,
                    kime_id = gorev.kime_id,
                    konu = gorev.konu,
                    aciklama = gorev.aciklama,
                    grup = gorev.grup,
                    durum = gorev.durum,
                    son_tarih = gorev.son_tarih,
                    yapilma_tarihi = gorev.yapilma_tarihi,
                };

                conn.Execute(sorgu, prms);
            }
            return Ok();
            //}
            //catch (Exception ex1)
            //{
            //  return BadRequest("API HATASI: " + ex1.Message);
            //}
        }

    }
}
