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
    public class TreinamentosController : ApiController
    {
        private SIGEWebApiContext db = new SIGEWebApiContext();

        // GET: api/Treinamentos
        /// <summary>
        /// retorna todos os treinamentos 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Treinamento> GetTreinamentos()
        {
            return db.Treinamentos;
        }

        // GET: api/Treinamentos/5
        /// <summary>
        /// retorna um treinamento especifico 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Treinamento))]
        public async Task<IHttpActionResult> GetTreinamento(int id)
        {
            Treinamento treinamento = await db.Treinamentos.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }

            return Ok(treinamento);
        }

        // PUT: api/Treinamentos/5
        /// <summary>
        /// Atualiza um treinamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="treinamento"></param>
        /// <returns></returns>
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

        // POST: api/Treinamentos
        /// <summary>
        /// Cria um treinamento
        /// </summary>
        /// <param name="treinamento"></param>
        /// <returns></returns>
        [ResponseType(typeof(Treinamento))]
        public async Task<IHttpActionResult> PostTreinamento(Treinamento treinamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treinamentos.Add(treinamento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = treinamento.Id }, treinamento);
        }

        // DELETE: api/Treinamentos/5
        /// <summary>
        /// Deleta um treinamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Treinamento))]
        public async Task<IHttpActionResult> DeleteTreinamento(int id)
        {
            Treinamento treinamento = await db.Treinamentos.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }

            db.Treinamentos.Remove(treinamento);
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
            return db.Treinamentos.Count(e => e.Id == id) > 0;
        }
    }
}