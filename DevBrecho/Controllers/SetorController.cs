using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevBrecho.Data;
using DevBrecho.Models;

namespace DevBrecho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase // Herdar de ControllerBase é o ideal para APIs
    {
        private readonly AppDbContext _context;

        public SetorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Setor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setor>>> GetSetores()
        {
            return await _context.Setores.ToListAsync();
        }

        // GET: api/Setor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Setor>> GetSetor(int id)
        {
            var setor = await _context.Setores.FindAsync(id);
            return setor == null ? NotFound() : Ok(setor);
        }

        // POST: api/Setor
        [HttpPost]
        public async Task<ActionResult<Setor>> CreateSetor([FromBody] Setor setor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Add(setor);
            await _context.SaveChangesAsync();

            // Retorna 201 Created, a melhor prática para criação
            return CreatedAtAction(nameof(GetSetor), new { id = setor.SetorId }, setor);
        }

        // PUT: api/Setor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetor(int id, [FromBody] Setor setor)
        {
            if (id != setor.SetorId) return BadRequest();
            _context.Entry(setor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Setor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {
            var setor = await _context.Setores.FindAsync(id);
            if (setor == null) return NotFound();

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