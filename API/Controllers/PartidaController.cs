using Microsoft.AspNetCore.Mvc;
using BTB.Repository.Interfaces;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PartidaController : ControllerBase
{
    private readonly IPartidaRepository _repository;

    public PartidaController(IPartidaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPartidas()
    {
        var partidas = await _repository.GetPartidasAsync();
        return Ok(partidas);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetPartida(string id)
    {
        var partida = await _repository.GetPartidaByIdAsync(id);
        if (partida == null) return NotFound();
        return Ok(partida);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePartida([FromBody] PartidaDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var partida = MapDtoToModel(dto);
        await _repository.PostPartidaAsync(partida);
        return CreatedAtAction(nameof(GetPartida), new { id = partida.IdPartida }, partida);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdatePartida(string id, [FromBody] PartidaDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var partida = MapDtoToModel(dto);
        partida.IdPartida = id;
        var result = await _repository.PutPartidaAsync(partida);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePartida(string id)
    {
        var result = await _repository.DeletePartidaAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    private Partida MapDtoToModel(PartidaDTOIn dto)
    {
        var partida = new Partida
        {
            IdPartida = dto.IdPartida ?? Guid.NewGuid().ToString(),
            ArrUsuario = dto.ArrUsuario?.Select(u => new Usuario { Id = u.Id ?? 0, Nombre = u.Nombre ?? string.Empty, Correo = u.Correo ?? string.Empty }).ToArray() ?? new Usuario[0],
            LstNodos = dto.LstNodos?.Select(n => new Nodo { IdNodo = (byte)n.IdNodo, ArrTropas = (n.ArrTropas ?? new List<TropaDTOIn>()).Select(t => new Tropa { Nombre = t.Nombre ?? string.Empty, Vida = t.Vida, Damage = t.Damage }).ToArray(), DuenoNodo = n.DuenoNodo == null ? null : new Usuario { Id = n.DuenoNodo.Id ?? 0, Nombre = n.DuenoNodo.Nombre ?? string.Empty, Correo = n.DuenoNodo.Correo ?? string.Empty } }).ToList() ?? new List<Nodo>()
        };

        return partida;
    }
}
