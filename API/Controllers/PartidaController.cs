using Microsoft.AspNetCore.Mvc;
using BTB.Service;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PartidaController : ControllerBase
{
    private readonly IPartidaService _partidaService;

    public PartidaController(IPartidaService partidaService)
    {
        _partidaService = partidaService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPartidas()
    {
        var partidas = await _partidaService.GetPartidasAsync();
        return Ok(partidas);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetPartida(string id)
    {
        var partida = await _partidaService.GetPartidaByIdAsync(id);
        if (partida == null) return NotFound();
        return Ok(partida);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePartida([FromBody] PartidaDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _partidaService.AddPartidaAsync(dto);
        return CreatedAtAction(nameof(GetPartida), new { id = created.IdPartida }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdatePartida(string id, [FromBody] PartidaDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _partidaService.UpdatePartidaAsync(id, dto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePartida(string id)
    {
        var result = await _partidaService.DeletePartidaAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    // Mappings moved to service
}
