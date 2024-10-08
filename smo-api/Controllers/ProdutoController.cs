﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using smo_api.Models;
using smo_api.Repositories;
using smo_api.Settings;

namespace smo_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly ProdutoRepository _produtoRepository;

    public ProdutoController(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var settings = mongoDbSettings.Value;
        _produtoRepository = new ProdutoRepository(
            settings.ConnectionString!,
            settings.DatabaseName!,
            settings.CollectionName!
        );
    }

    [HttpGet]
    public async Task<ActionResult<List<ProdutoModel>>> GetProdutos()
    {
        var produtos = await _produtoRepository.GetProdutosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoModel>> GetProduto(string id)
    {
        var produto = await _produtoRepository.GetProdutoByIdAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }
}