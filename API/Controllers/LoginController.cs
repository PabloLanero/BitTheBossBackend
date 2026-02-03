using Microsoft.AspNetCore.Mvc;
using BTB.Entities.DTO;
using BTB.Service;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
	private readonly IAuthService _authService;

	public LoginController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginDtoIn dto)
	{
		try
		{
			var token = _authService.Login(dto);
			return Ok(new { token });
		}
		catch (Exception ex)
		{
			return Unauthorized(new { message = ex.Message });
		}
	}

	[HttpPost("register")]
	public IActionResult Register([FromBody] UserDTOIn dto)
	{
		try
		{
			var token = _authService.Register(dto);
			return Ok(new { token });
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
