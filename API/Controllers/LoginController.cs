using Microsoft.AspNetCore.Mvc;
using BTB.Entities.DTO;
using BTB.Service;
using BTB.Service.Common;
using Microsoft.AspNetCore.Authorization;

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
	[AllowAnonymous]
	public IActionResult Login([FromBody] LoginDtoIn dto)
	{
		try
		{
			var token = _authService.Login(dto);
			return Ok(new { token });
		}
		catch (ValidationException vex)
		{
			return BadRequest(new { message = vex.Message });
		}
		catch (BusinessException bex)
		{
			return Unauthorized(new { message = bex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = ex.Message });
		}
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public IActionResult Register([FromBody] UserDTOIn dto)
	{
		try
		{
			var token = _authService.Register(dto);
			return Ok(new { token });
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
