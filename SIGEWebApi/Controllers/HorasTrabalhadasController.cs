using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SIGEWebApi.Models;

namespace SIGEWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class HorasTrabalhadasController : ApiController
    {
        private SIGEWebApiContext db = new SIGEWebApiContext();

        // GET: api/HorasTrabalhadas
        public IQueryable<HorasTrabalhadas> GetHorasTrabalhadas()
        {
            return db.HorasTrabalhadas;
        }

        // GET: api/HorasTrabalhadas/5
        [ResponseType(typeof(HorasTrabalhadas))]
        public async Task<IHttpActionResult> GetHorasTrabalhadas(int id)
        {
            HorasTrabalhadas horasTrabalhadas = await db.HorasTrabalhadas.FindAsync(id);
            if (horasTrabalhadas == null)
            {
                return NotFound();
            }

            return Ok(horasTrabalhadas);
        }
        // GET: api/HorasTrabalhadas/5
        [Route("api/HorasTrabalhadas/mes/{mes}")]
        [ResponseType(typeof(List<HorasTrabalhadas>))]
        public async Task<IHttpActionResult> GetHorasTrabalhadasMes(int mes)
        {
            List<HorasTrabalhadas> horasTrabalhadas = await db.HorasTrabalhadas.Where(h => h.Mes == mes).ToListAsync();
            if (horasTrabalhadas == null)
            {
                return NotFound();
            }

            return Ok(horasTrabalhadas);
        }
        // PUT: api/HorasTrabalhadas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHorasTrabalhadas(int id, HorasTrabalhadas horasTrabalhadas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horasTrabalhadas.Id)
            {
                return BadRequest();
            }

            db.Entry(horasTrabalhadas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorasTrabalhadasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HorasTrabalhadas
        [ResponseType(typeof(HorasTrabalhadas))]
        public async Task<IHttpActionResult> PostHorasTrabalhadas(HorasTrabalhadas horasTrabalhadas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HorasTrabalhadas.Add(horasTrabalhadas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = horasTrabalhadas.Id }, horasTrabalhadas);
        }

        // DELETE: api/HorasTrabalhadas/5
        [ResponseType(typeof(HorasTrabalhadas))]
        public async Task<IHttpActionResult> DeleteHorasTrabalhadas(int id)
        {
            HorasTrabalhadas horasTrabalhadas = await db.HorasTrabalhadas.FindAsync(id);
            if (horasTrabalhadas == null)
            {
                return NotFound();
            }

            db.HorasTrabalhadas.Remove(horasTrabalhadas);
            await db.SaveChangesAsync();

            return Ok(horasTrabalhadas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HorasTrabalhadasExists(int id)
        {
            return db.HorasTrabalhadas.Count(e => e.Id == id) > 0;
        }
    }
}