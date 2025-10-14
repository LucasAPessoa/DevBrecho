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
    public class PecaCadastradaController : Controller
    {
        private readonly AppDbContext _context;

        public PecaCadastradaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PecaCadastrada
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PecasCadastradas.Include(p => p.Bolsa);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PecaCadastrada/Details/5
        [HttpGet]
        [Route("api/[controller]/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pecaCadastrada = await _context.PecasCadastradas
                .Include(p => p.Bolsa)
                .FirstOrDefaultAsync(m => m.PecaCadastradaId == id);
            if (pecaCadastrada == null)
            {
                return NotFound();
            }

            return Ok(pecaCadastrada);
        }

        
        // POST: PecaCadastrada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create([FromBody] PecaCadastrada pecaCadastrada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pecaCadastrada);
                await _context.SaveChangesAsync();
                return Ok(pecaCadastrada);
            }
            
            return BadRequest(ModelState);
        }

        

        // PUT: PecaCadastrada/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PecaCadastrada pecaCadastrada)
        {
            if (id != pecaCadastrada.PecaCadastradaId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(pecaCadastrada).State = EntityState.Modified;

      
                try
                {
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PecaCadastradaExists(pecaCadastrada.PecaCadastradaId))
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



        // DELETE: PecaCadastrada/Delete/5
        [HttpDelete, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pecaCadastrada = await _context.PecasCadastradas.FindAsync(id);
            if (pecaCadastrada == null)
            {
               return NotFound();
            }

            _context.PecasCadastradas.Remove(pecaCadastrada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PecaCadastradaExists(int id)
        {
            return _context.PecasCadastradas.Any(e => e.PecaCadastradaId == id);
        }
    }
}
