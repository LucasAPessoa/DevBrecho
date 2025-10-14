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
    public class SetorController : Controller
    {
        private readonly AppDbContext _context;

        public SetorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Setor
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Setores.ToListAsync());
        }

        // GET: Setor/Details/5
        [HttpGet]
        [Route("api/[controller]/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setor = await _context.Setores
                .FirstOrDefaultAsync(m => m.SetorId == id);
            if (setor == null)
            {
                return NotFound();
            }

            return Ok(setor);
        }

      

        // POST: Setor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create([FromBody] Setor setor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setor);
                await _context.SaveChangesAsync();
                return Ok(setor);
            }
            return BadRequest(ModelState);
        }

       
        // PUT: Setor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Setor setor)
        {
            if (id != setor.SetorId)
            {
                return BadRequest(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(setor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id))
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

       

        // DELETE: Setor/Delete/5
        [HttpDelete, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Route("api/[controller]/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setor = await _context.Setores.FindAsync(id);
            if (setor == null)
            {
                return NotFound(); 
            }

            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();

            
            return NoContent();
        }

        private bool SetorExists(int id)
        {
            return _context.Setores.Any(e => e.SetorId == id);
        }
    }
}
