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
    public class FornecedoraController : Controller
    {
        private readonly AppDbContext _context;

        public FornecedoraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Fornecedora
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Fornecedoras.ToListAsync());
        }

        // GET: Fornecedora/Details/5
        [HttpGet]
        [Route("api/[controller]/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedora = await _context.Fornecedoras
                .FirstOrDefaultAsync(m => m.FornecedoraId == id);
            if (fornecedora == null)
            {
                return NotFound();
            }

            return Ok(fornecedora);
        }


        // POST: Fornecedora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create([FromBody] Fornecedora fornecedora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fornecedora);
                await _context.SaveChangesAsync();
                return Ok(fornecedora);
            }
            return BadRequest(ModelState);
        }

       
        // PUT: Fornecedora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Fornecedora fornecedora)
        {
            if (id != fornecedora.FornecedoraId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(fornecedora).State = EntityState.Modified;

            try
                {
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedoraExists(fornecedora.FornecedoraId))
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


        // DELETE: Fornecedora/Delete/5

        [HttpDelete, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedora = await _context.Fornecedoras.FindAsync(id);
            if (fornecedora == null)
            {
               return NotFound();
            }

            _context.Fornecedoras.Remove(fornecedora);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool FornecedoraExists(int id)
        {
            return _context.Fornecedoras.Any(e => e.FornecedoraId == id);
        }
    }
}
