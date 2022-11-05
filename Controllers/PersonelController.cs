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
    public class PersonelController : Controller
    {

        string conString = "DATA SOURCE = BARIS; INITIAL CATALOG = GorevTakipDB; USER ID = sa; PASSWORD = 123; Trusted_Connection = true;";


        [HttpGet("liste")]
        public IActionResult PersonelListele()
        {
            string sorgu = "";

            List<Personel> personelList = new List<Personel>();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                sorgu = "SELECT * FROM tbl_personel";
                personelList = conn.Query<Personel>(sorgu).ToList();
            }

            return Ok(personelList);
        }




        public int LoginKontrol(string gelenEmail, string gelenSifre)
        {
            string sorgu = "select * from tbl_personel where email ='" + gelenEmail + "' and sifre ='" + gelenSifre + "'";
            SqlCommand cmd;
            SqlDataReader dr;

            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            cmd = new SqlCommand(sorgu, conn);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                conn.Close();
                return 1;
            }
            else
            {
                conn.Close();
                return 0;
            }
        }



        [HttpGet("loginControl")]
        public IActionResult PersonelListelee(string email, string sifre)
        {
            string sorgu = "";

            using (SqlConnection conn = new SqlConnection(conString))
            {
                sorgu = "SELECT * FROM tbl_personel";
                conn.Query<Personel>(sorgu).ToList();
            }

            return Ok(LoginKontrol(email, sifre));
        }

    }
}
