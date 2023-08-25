// CommentsController.cs
using Microsoft.AspNetCore.Mvc;
using KutuphaneAPI;
using Microsoft.EntityFrameworkCore;
using KutuphaneAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly veritabani _context;

    public CommentsController(veritabani context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Yorum>> GetComments()
    {
        return _context.Yorumlar.ToList();
    }

    [HttpPost]
    public ActionResult<Yorum> AddComment(Yorum comment)
    {
        _context.Yorumlar.Add(comment);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetComments), new { id = comment.id }, comment);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteComment(int id)
    {
        var comment = _context.Yorumlar.Find(id);
        if (comment == null)
        {
            return NotFound();
        }

        _context.Yorumlar.Remove(comment);
        _context.SaveChanges();
        return NoContent();
    }
}