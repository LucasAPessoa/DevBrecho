using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevBrecho.Data;
using DevBrecho.Models;

namespace DevBrecho.Controllers
{
    public class BolsaController : Controller
    {
        private readonly AppDbContext _context;

        public BolsaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bolsa
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Bolsas.Include(b => b.Fornecedora).Include(b => b.Setor);
            return Ok(await appDbContext.ToListAsync());
        }

        // GET: Bolsa/Details/5
        [Route("api/[controller]/Details/{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolsa = await _context.Bolsas
                .Include(b => b.Fornecedora)
                .Include(b => b.Setor)
                .FirstOrDefaultAsync(m => m.BolsaId == id);
            if (bolsa == null)
            {
                return NotFound();
            }

            return Ok(bolsa);
        }

        // POST: Bolsa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create([FromBody] Bolsa bolsa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolsa);
                await _context.SaveChangesAsync();
                return Ok(bolsa);
            }
          
            return BadRequest(ModelState);
        }


        // PUT: Bolsa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("BolsaId,DataDeEntrada,DataMensagem,FornecedoraId,SetorId,QuantidadeDePecasSemCadastro,Observacoes")] Bolsa bolsa)
        {
            if (id != bolsa.BolsaId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(bolsa).State = EntityState.Modified;

            try
                {
                 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolsaExists(bolsa.BolsaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
           
          
           
        }

       

        // DELETE: Bolsa/Delete/5
        [HttpDelete, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolsa = await _context.Bolsas.FindAsync(id);
            if (bolsa == null)
            {
                return NotFound();
            }
            _context.Bolsas.Remove(bolsa);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool BolsaExists(int id)
        {
            return _context.Bolsas.Any(e => e.BolsaId == id);
        }
    }
}
