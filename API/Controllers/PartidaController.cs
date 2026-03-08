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
        try
        {
            var partidas = await _partidaService.GetPartidasAsync();
            return Ok(partidas);
        }
        catch (BTB.Service.Common.ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BTB.Service.Common.BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetPartida(string id)
    {
        try
        {
            var partida = await _partidaService.GetPartidaByIdAsync(id);
            if (partida == null) return NotFound();
            return Ok(partida);
        }
        catch (BTB.Service.Common.NotFoundException nf)
        {
            return NotFound(new { message = nf.Message });
        }
        catch (BTB.Service.Common.ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BTB.Service.Common.BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePartida([FromBody] PartidaDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var created = await _partidaService.AddPartidaAsync(dto);
            return Ok(created);
        }
        catch (BTB.Service.Common.ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BTB.Service.Common.BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdatePartida(string id, [FromBody] PartidaDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var result = await _partidaService.UpdatePartidaAsync(id, dto);
            if (string.IsNullOrWhiteSpace(result.IdPartida)) return NotFound();
            return NoContent();
        }
        catch (BTB.Service.Common.NotFoundException nf)
        {
            return NotFound(new { message = nf.Message });
        }
        catch (BTB.Service.Common.ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BTB.Service.Common.BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeletePartida(string id)
    {
        try
        {
            var result = await _partidaService.DeletePartidaAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (BTB.Service.Common.ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BTB.Service.Common.BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
