using FilmesApi.Controllers;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class FilmeControllerTests
{
    private FilmeContext GetContext()
    {
        var options = new DbContextOptionsBuilder<FilmeContext>()
            .UseInMemoryDatabase(databaseName: "FilmesApiFilmeTestDb")
            .Options;
        return new FilmeContext(options);
    }

    private IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<CreateFilmeDto, Filme>();
            cfg.CreateMap<UpdateFilmeDto, Filme>();
            cfg.CreateMap<Filme, UpdateFilmeDto>();
            cfg.CreateMap<Filme, ReadeFilmeDto>();
        });
        return config.CreateMapper();
    }

    [Fact]
    public void AdicionaFilme_DeveAdicionarFilme()
    {
        var context = GetContext();
        var mapper = GetMapper();
        var controller = new FilmeController(context, mapper);

        var dto = new CreateFilmeDto { Titulo = "Teste", Duracao = 120, Genero = "Ação", Diretor = "Alguém" };
        var result = controller.AdicionaFilme(dto);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void RecuperaFilmes_DeveRetornarLista()
    {
        var context = GetContext();
        var mapper = GetMapper();
        context.Filmes.Add(new Filme { Titulo = "Teste", Duracao = 120, Genero = "Ação", Diretor = "Alguém" });
        context.SaveChanges();
        var controller = new FilmeController(context, mapper);

        var result = controller.RecuperaFilmes();
        Assert.NotEmpty(result);
    }
}