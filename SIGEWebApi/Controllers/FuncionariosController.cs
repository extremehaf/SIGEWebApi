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
using SIGEWebApi.Filters;
using SIGEWebApi.Models;

namespace SIGEWebApi.Controllers
{
    public class FuncionariosController : ApiController
    {
        private SIGEWebApiContext db = new SIGEWebApiContext();

        // GET: api/Funcionarios
        /// <summary>
        /// Lista todos os funcionarios 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Funcionario> GetFuncionarios()
        {
            return db.Funcionarios.Include(h => h.HorasTrabalhadas);
        }
        // GET: api/Funcionarios
        /// <summary>
        /// Lista todos os funcionarios por turno 
        /// </summary>
        /// <param name="turno">Matutino ou Noturno</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<Funcionario>))]
        public IQueryable<Funcionario> GetFuncionarios(string turno)
        {
            return db.Funcionarios.Include(h => h.HorasTrabalhadas).Where(f => f.Turno == turno);
        }

        // GET: api/Funcionarios/5
        /// <summary>
        /// Lista um funcionario especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Funcionario))]
        [BasicAuthenticationFilter(false)]
        public async Task<IHttpActionResult> GetFuncionario(int id)
        {
            Funcionario funcionario = await db.Funcionarios.Include(h => h.HorasTrabalhadas).FirstOrDefaultAsync(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionarios/5
        /// <summary>
        /// Atualiza um funcionario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="funcionario"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [BasicAuthenticationFilter(true)]
        public async Task<IHttpActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            db.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // POST: api/Funcionarios
        /// <summary>
        /// Cadastra um funcionario
        /// </summary>
        /// <param name="funcionario"></param>
        /// <returns></returns>
        [ResponseType(typeof(Funcionario))]
        [BasicAuthenticationFilter(true)]
        public async Task<IHttpActionResult> PostFuncionario(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Funcionarios.Add(funcionario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = funcionario.Id }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        /// <summary>
        /// Deleta um funcionario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Funcionario))]
        [BasicAuthenticationFilter(true)]
        public async Task<IHttpActionResult> DeleteFuncionario(int id)
        {
            Funcionario funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            db.Funcionarios.Remove(funcionario);
            await db.SaveChangesAsync();

            return Ok(funcionario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionarioExists(int id)
        {
            return db.Funcionarios.Count(e => e.Id == id) > 0;
        }


    }
}