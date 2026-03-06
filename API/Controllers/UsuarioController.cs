using Microsoft.AspNetCore.Mvc;
using BTB.Service;
using BTB.Entities.Models;
using BTB.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using BTB.Service.Common;
using System.Threading.Tasks;
using System.Security.Claims;
using System.ComponentModel;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsuarios()
    {
        try
        {
            var usuarios = await _usuarioService.GetUsuarioById(Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            return Ok(usuarios);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BusinessException bex)
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
    public async Task<IActionResult> GetUsuario(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
        catch (NotFoundException nf)
        {
            return NotFound(new { message = nf.Message });
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BusinessException bex)
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
    public async Task<IActionResult> CreateUsuario([FromBody] UserDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var userOut = await _usuarioService.AddUserFromCredentials(dto);
            return CreatedAtAction(nameof(GetUsuario), new { id = userOut.UserId ?? 0 }, userOut);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("fromcredentials")]
    [Authorize]
    public IActionResult CreateUsuarioFromCredentials([FromBody] UserDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var userOut = _usuarioService.AddUserFromCredentials(dto);
            return Ok(userOut);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BusinessException bex)
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
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
    {
        try
        {
            var result = await _usuarioService.UpdateUsuario(usuario);
            if (result.UsuarioId <=0) return BadRequest(new { message = "No se pudo actualizar el usuario" });
            return NoContent();
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BusinessException bex)
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
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        try
        {
            var result = await _usuarioService.DeleteUsuario(id);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch (BusinessException bex)
        {
            return BadRequest(new { message = bex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
