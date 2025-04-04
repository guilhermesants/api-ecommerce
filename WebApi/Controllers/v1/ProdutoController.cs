using Application.Common.Responses;
using Application.UseCases.AdicionarProduto;
using Application.UseCases.RemoverProduto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers.v1;

[Route("api/v1/[controller]")]

public class ProdutoController : ControllerBase
{
    private readonly ISender _sender;

    public ProdutoController(ISender sender) => _sender = sender;

    [HttpPost]
    [EndpointSummary("Novo Produto")]
    [EndpointDescription("Responsável por cadastrar um novo produto e a categoria caso não exista")]
    [ProducesResponseType(typeof(Result<NewProductCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarProduto([FromBody] NewProductCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        return Created(string.Empty, result);
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Deletar Produto")]
    [EndpointDescription("Responsável por remover um produto pelo id")]
    [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletarProduto([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new RemoveProductCommand(id), cancellationToken);
        return StatusCode((int)result.HttpStatusCode);
    }
}
