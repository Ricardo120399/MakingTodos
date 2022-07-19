using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakingTodos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly DataContext _context;
        public NotesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Notes>>> Get()
        {
            return Ok(await _context.Notess.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Notes>>> Get(int id)
        {
            var note = await _context.Notess.FindAsync(id);
            if (note == null)
                return BadRequest("Note not found");
            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<List<Notes>>> AddNote(Notes note)
        {
            _context.Notess.Add(note);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notess.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Notes>>> UpdateNotes(Notes request)
        {
            var dbNote = await _context.Notess.FindAsync(request.Id);
            if (dbNote == null)
                return BadRequest("Note not found");

            dbNote.Subject = request.Subject;
            dbNote.Teacher = request.Teacher;
            dbNote.Qualification = request.Qualification;

            await _context.SaveChangesAsync();

            return Ok(await _context.Notess.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Notes>>> Delete(int id)
        {
            var dbNote = await _context.Notess.FindAsync(id);
            if (dbNote == null)
                return BadRequest("Note not found");

            _context.Notess.Remove(dbNote);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notess.ToListAsync());

        }
    }
}
