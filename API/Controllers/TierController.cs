using Microsoft.AspNetCore.Mvc;
using BTB.Service;
using BTB.Entities.DTO;
using BTB.Service.Common;
using Microsoft.AspNetCore.Authorization;
using BTB.Entities.Models;

[ApiController]
[Route("[controller]")]
public class TierController : ControllerBase
{
    private readonly ITierService _service;

    public TierController(ITierService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTiers()
    {
        try
        {
            var lst = await _service.GetTiersAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetTier(int id)
    {
        try
        {
            var t = await _service.GetTierByIdAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
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
    public async Task<IActionResult> CreateTier([FromBody] TierDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var created = await _service.AddTierAsync(dto);
            return CreatedAtAction(nameof(CreateTier), new { id = created.Id }, created);
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
    public async Task<ActionResult<Tier>> UpdateTier(int id, [FromBody] TierDTOIn dto)
    {
        try
        {
            Tier result = await _service.UpdateTierAsync(id, dto);
            if (result == null) return BadRequest(new { message = "No se pudo actualizar el tier" });
            return CreatedAtAction(nameof(UpdateTier), result);
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
    public async Task<IActionResult> DeleteTier(int id)
    {
        try
        {
            var result = await _service.DeleteTierAsync(id);
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
