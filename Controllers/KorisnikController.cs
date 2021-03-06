using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers {


    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase {
        public KuvarContext Context { get; set; }

        public KorisnikController(KuvarContext context) {
            Context = context;
        }

        [Route("DodajKorisnika")]
        [HttpPost]
        public async Task<ActionResult> DodajKorisnika([FromBody] Korisnik korisnik) {

            var tmp = await Context.Korisnici.Where(k => k.Email == korisnik.Email).FirstOrDefaultAsync();

            if (tmp != null)
                return BadRequest("Taj korisnik već postoji");

            try {
                Context.Korisnici.Add(korisnik);
                await Context.SaveChangesAsync();
                return Ok(korisnik);
            } catch (Exception e) {
                return BadRequest(e.InnerException);
            }

        }



        [Route("Login/{email}/{password}")]
        [HttpGet]
        public async Task<ActionResult> UlogujSe(string email, string password) {
            try {

                var korisnik = await Context.Korisnici.Where(k => k.Email == email).FirstOrDefaultAsync();

                if (korisnik == null)
                    return BadRequest("Taj korisnik ne postoji!");

                Trace.WriteLine("" + korisnik.ID);

                if (korisnik.Password != password)
                    return BadRequest("Pogresna sifra");

                return Ok(korisnik);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }


//za vežbu
       /* [Route("Delete/{idKor}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiKorisnika(int idKor) {


            try {
                var korisnik = await Context.Korisnici.Where(k => k.ID == idKor).FirstOrDefaultAsync();

                if (korisnik == null)
                    return BadRequest("Korisnik ne postoji!");

                var recepti = await Context.Recepti.Where(r => r.Korisnik.ID == idKor).ToListAsync();
                foreach (var recept in recepti) {
                    var satojci = await Context.ReceptSastojak.Where(rs => rs.Recept.ID == recept.ID).ToListAsync();
                    foreach (var s in satojci)
                        Context.ReceptSastojak.Remove(s);

                    var koraci = await Context.Koraci.Where(k => k.Recept.ID == recept.ID).ToListAsync();
                    foreach (var k in koraci)
                        Context.Koraci.Remove(k);

                    var ocene = await Context.Ocene.Where(o => o.Recept.ID == recept.ID).ToListAsync();
                    foreach (var o in ocene)
                        Context.Ocene.Remove(o);

                    Context.Recepti.Remove(recept);
                }

                Context.Korisnici.Remove(korisnik);
                await Context.SaveChangesAsync();

                return Ok("Korisnik obrisan.");
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
 

        }
        */
        
    }
}
