using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote([FromBody] Note note) // Добавляем атрибут [FromBody]
        {
            if (note.Tags == null) // Проверяем, есть ли теги. Если нет, то присваиваем пустой список.
            {
                note.Tags = new List<Tag>();
            }

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }
        public class NoteUpdateDto
        {
            public string? Title { get; set; }
            public string? Content { get; set; }
            public DateTimeOffset? Reminder { get; set; }
            public List<Tag>? Tags { get; set; }
        }

        // PATCH: api/Notes/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchNote(int id, [FromBody] NoteUpdateDto noteUpdateDto)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            if (noteUpdateDto.Title != null)
            {
                note.Title = noteUpdateDto.Title;
            }

            if (noteUpdateDto.Content != null)
            {
                note.Content = noteUpdateDto.Content;
            }

            if (noteUpdateDto.Reminder.HasValue)
            {
                note.Reminder = noteUpdateDto.Reminder.Value;
            }

            if (noteUpdateDto.Tags != null)
            {
                note.Tags = noteUpdateDto.Tags;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
