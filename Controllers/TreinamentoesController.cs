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
using System.Web.Http.Description;
using SIGEWebApi.Models;

namespace SIGEWebApi.Controllers
{
    public class TreinamentoesController : ApiController
    {
        private SIGEWebApiContext db = new SIGEWebApiContext();

        // GET: api/Treinamentoes
        public IQueryable<Treinamento> GetTreinamentoes()
        {
            return db.Treinamentoes;
        }

        // GET: api/Treinamentoes/5
        [ResponseType(typeof(Treinamento))]
        public async Task<IHttpActionResult> GetTreinamento(int id)
        {
            Treinamento treinamento = await db.Treinamentoes.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }

            return Ok(treinamento);
        }

        // PUT: api/Treinamentoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTreinamento(int id, Treinamento treinamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treinamento.Id)
            {
                return BadRequest();
            }

            db.Entry(treinamento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinamentoExists(id))
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

        // POST: api/Treinamentoes
        [ResponseType(typeof(Treinamento))]
        public async Task<IHttpActionResult> PostTreinamento(Treinamento treinamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treinamentoes.Add(treinamento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = treinamento.Id }, treinamento);
        }

        // DELETE: api/Treinamentoes/5
        [ResponseType(typeof(Treinamento))]
        public async Task<IHttpActionResult> DeleteTreinamento(int id)
        {
            Treinamento treinamento = await db.Treinamentoes.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }

            db.Treinamentoes.Remove(treinamento);
            await db.SaveChangesAsync();

            return Ok(treinamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreinamentoExists(int id)
        {
            return db.Treinamentoes.Count(e => e.Id == id) > 0;
        }
    }
}