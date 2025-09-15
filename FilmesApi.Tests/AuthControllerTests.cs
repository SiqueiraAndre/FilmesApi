using FilmesApi.Controllers;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using Xunit;

public class AuthControllerTests
{
    private FilmeContext GetContext()
    {
        var options = new DbContextOptionsBuilder<FilmeContext>()
            .UseInMemoryDatabase(databaseName: "FilmesApiTestDb")
            .Options;
        return new FilmeContext(options);
    }

    private IConfiguration GetConfig()
    {
        var inMemorySettings = new Dictionary<string, string?> {
            {"Jwt:Key", "test-key-test-key-test-key-test-key-test-key-test-key"},
            {"Jwt:Issuer", "FilmesApi"},
            {"Jwt:Audience", "FilmesApi"}
        };
        return new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
    }

    [Fact]
    public void Register_DeveRegistrarUsuario()
    {
        var context = GetContext();
        var config = GetConfig();
        var controller = new AuthController(context, config);

        var result = controller.Register(new UsuarioDto { Username = "user", Password = "pass" });
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Register_DeveRetornarBadRequest_SeUsuarioExistir()
    {
        var context = GetContext();
        var config = GetConfig();
        context.Usuarios.Add(new Usuario { Username = "user", Password = "pass" });
        context.SaveChanges();
        var controller = new AuthController(context, config);

        var result = controller.Register(new UsuarioDto { Username = "user", Password = "pass" });
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Login_DeveRetornarToken_SeCredenciaisValidas()
    {
        var context = GetContext();
        var config = GetConfig();
        var senhaHash = BCrypt.Net.BCrypt.HashPassword("pass");
        context.Usuarios.Add(new Usuario { Username = "user", Password = senhaHash });
        context.SaveChanges();
        var controller = new AuthController(context, config);

        var result = controller.Login(new UsuarioDto { Username = "user", Password = "pass" });
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = okResult.Value?.ToString() ?? string.Empty;
        Assert.Contains("Token", value);
    }

    [Fact]
    public void Login_DeveRetornarUnauthorized_SeCredenciaisInvalidas()
    {
        var context = GetContext();
        var config = GetConfig();
        var controller = new AuthController(context, config);

        var result = controller.Login(new UsuarioDto { Username = "user", Password = "wrong" });
        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}