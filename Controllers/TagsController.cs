using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TagsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Tag>> GetTags()
    {
        var tags = _context.Tags.ToList();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
        {
            return NotFound();
        }

        return tag;
    }

    public class TagCreationDto
    {
        public int NoteId { get; set; }
        public string Name { get; set; }
    }


    [HttpPost]
    public async Task<ActionResult<Tag>> PostTag([FromBody] TagCreationDto tagDto)
    {
        var note = await _context.Notes.FindAsync(tagDto.NoteId);
        if (note == null)
        {
            return NotFound("Note with the specified ID not found.");
        }

        var tag = new Tag { Name = tagDto.Name };
        note.Tags.Add(tag);

        await _context.SaveChangesAsync();

        // Instead of returning the entire list of tags, return only the newly created tag
        return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
    }

    public class TagUpdateDto
    {
        public string Name { get; set; }
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchTag(int id, [FromBody] TagUpdateDto tagUpdateDto)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        if (tagUpdateDto.Name != null)
        {
            tag.Name = tagUpdateDto.Name;
        }

        // Apply changes to associated notes' Tags collection
        foreach (var note in tag.Notes)
        {
            if (tagUpdateDto.Name != null)
            {
                note.Tags.First(t => t.Id == tag.Id).Name = tagUpdateDto.Name;
            }
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        // Remove the tag from associated notes' Tags collection
        foreach (var note in tag.Notes.ToList())
        {
            note.Tags.Remove(tag);
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TagExists(int id)
    {
        return _context.Tags.Any(e => e.Id == id);
    }
}
