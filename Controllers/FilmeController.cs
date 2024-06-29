using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.Id}, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromHeader] int skip = 0, [FromHeader] int take = 50) {
        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = GetFilme(id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

    private Filme? GetFilme(int id)
    {
        return _context.Filmes.FirstOrDefault(filme => filme.Id == id);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = GetFilme(id);
        if (filme == null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return Ok();

    }
}
