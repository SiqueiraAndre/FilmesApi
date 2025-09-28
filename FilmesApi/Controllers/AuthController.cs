using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly FilmeContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(FilmeContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Registra um novo usuário na aplicação
    /// </summary>
    /// <param name="usuarioDto">Dados do usuário para registro</param>
    /// <returns>Mensagem de sucesso ou erro</returns>
    /// <response code="200">Usuário registrado com sucesso</response>
    /// <response code="400">Usuário já existe</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] UsuarioDto usuarioDto)
    {
        if (_context.Usuarios.Any(u => u.Username == usuarioDto.Username))
            return BadRequest("Usuário já existe.");

        var usuario = new Usuario
        {
            Username = usuarioDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password) // Criptografa a senha
        };
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        // Gera o token JWT para o usuário recém-registrado
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    /// <summary>
    /// Realiza login e retorna um token JWT para autenticação
    /// </summary>
    /// <param name="usuarioDto">Dados do usuário para login</param>
    /// <returns>Token JWT</returns>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="401">Usuário ou senha inválidos</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] UsuarioDto usuarioDto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Username == usuarioDto.Username);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(usuarioDto.Password, usuario.Password))
            return Unauthorized("Usuário ou senha inválidos.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}