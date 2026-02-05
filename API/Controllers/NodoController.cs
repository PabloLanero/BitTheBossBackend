using Microsoft.AspNetCore.Mvc;
using BTB.Service;
using BTB.Entities.DTO;
using BTB.Service.Common;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
public class NodoController : ControllerBase
{
    private readonly INodoService _service;

    public NodoController(INodoService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetNodos()
    {
        try
        {
            var lst = await _service.GetNodosAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetNodo(int id)
    {
        try
        {
            var n = await _service.GetNodoByIdAsync(id);
            if (n == null) return NotFound();
            return Ok(n);
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
    public async Task<IActionResult> CreateNodo([FromBody] NodoDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var created = await _service.AddNodoAsync(dto);
            return CreatedAtAction(nameof(GetNodo), new { id = created.IdNodo }, created);
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

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateNodo(int id, [FromBody] NodoDTOIn dto)
    {
        try
        {
            var result = await _service.UpdateNodoAsync(id, dto);
            if (!result) return BadRequest(new { message = "No se pudo actualizar el nodo" });
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

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteNodo(int id)
    {
        try
        {
            var result = await _service.DeleteNodoAsync(id);
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
