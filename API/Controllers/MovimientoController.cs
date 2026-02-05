using Microsoft.AspNetCore.Mvc;
using BTB.Service;
using BTB.Entities.DTO;
using BTB.Service.Common;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
public class MovimientoController : ControllerBase
{
    private readonly IMovimientoService _service;

    public MovimientoController(IMovimientoService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMovimientos()
    {
        try
        {
            var lst = await _service.GetMovimientosAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetMovimiento(int id)
    {
        try
        {
            var m = await _service.GetMovimientoByIdAsync(id);
            if (m == null) return NotFound();
            return Ok(m);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMovimiento([FromBody] MovimientoDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var created = await _service.AddMovimientoAsync(dto);
            return CreatedAtAction(nameof(GetMovimiento), new { id = created.Id }, created);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMovimiento(int id)
    {
        try
        {
            var result = await _service.DeleteMovimientoAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
