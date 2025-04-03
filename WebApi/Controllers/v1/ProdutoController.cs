using Application.Common.Responses;
using Application.UseCases.AdicionarProduto;
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
    [ProducesResponseType(typeof(Result<NewProductCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarProduto([FromBody] NewProductCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.HttpStatusCode == 0 ? (int)HttpStatusCode.InternalServerError : (int)result.HttpStatusCode, result.ErrorMessage);

        return Ok(result);
    }
}
