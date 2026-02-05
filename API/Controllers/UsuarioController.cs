using Microsoft.AspNetCore.Mvc;
using BTB.Service;
using BTB.Entities.Models;
using BTB.Entities.DTO;
using Microsoft.AspNetCore.Authorization;

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
        var usuarios = await _usuarioService.GetUsuariosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _usuarioService.GetUsuarioById(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateUsuario([FromBody] UserDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userOut = _usuarioService.AddUserFromCredentials(dto);
        return CreatedAtAction(nameof(GetUsuario), new { id = userOut.UserId ?? 0 }, userOut);
    }

    [HttpPost("fromcredentials")]
    [Authorize]
    public IActionResult CreateUsuarioFromCredentials([FromBody] UserDTOIn dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userOut = _usuarioService.AddUserFromCredentials(dto);
        return Ok(userOut);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
    {
        // Optionally ensure id matches body if you have an Id property
        var result = await _usuarioService.UpdateUsuario(usuario);
        if (!result) return BadRequest(new { message = "No se pudo actualizar el usuario" });
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var result = await _usuarioService.DeleteUsuario(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
