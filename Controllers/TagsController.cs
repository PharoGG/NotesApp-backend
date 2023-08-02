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


    [HttpPut("{id}")]
    public async Task<IActionResult> PutTag(int id, [FromBody] Tag tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }

        _context.Entry(tag).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TagExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
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
