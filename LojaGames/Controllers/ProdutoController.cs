using FluentValidation;
using LojaGames.Model;
using LojaGames.Service.Implements;
using Microsoft.AspNetCore.Mvc;

namespace LojaGames.Controllers
{
      [Route("~/produtos")]
      [ApiController]
        
    public class ProdutoController : ControllerBase
    {
       
            private readonly IProdutoService _produtoService;
            private readonly IValidator<Produto> _produtoValidator;

            public ProdutoController(
                IProdutoService produtoService,
                IValidator<Produto> produtoValidator)
            {
                _produtoService = produtoService;
                _produtoValidator = produtoValidator;
            }


            [HttpGet]
            public async Task<ActionResult> GetAll()
            {
                return Ok(await _produtoService.GetAll());

            }

            [HttpGet("{id}")]
            public async Task<ActionResult> GetById(long id)
            {
                var Resposta = await _produtoService.GetById(id);

                if (Resposta is null)
                    return NotFound();

                return Ok(Resposta);
            }

            [HttpGet("nome/{nome}/{console}")]
            public async Task<ActionResult> GetByNomeCon(string nome , string console)
            {
                return Ok(await _produtoService.GetByNomeCon(nome, console));

            }


            [HttpGet("preco/{numero1}/{numero2}")]
            public async Task<ActionResult> GetByPreco(decimal numero1,decimal numero2)
            {
                if (numero1>numero2)
                return BadRequest("Valor minimo não pode ser maior que valor máximo");

                var Resposta = await _produtoService.GetByPreco(numero1, numero2);
                    
                return Ok(Resposta);
            }

        [HttpPost]
            public async Task<ActionResult> Create([FromBody] Produto produto)
            {
                var validarProduto = await _produtoValidator.ValidateAsync(produto);

                if (!validarProduto.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

                var Resposta = await _produtoService.Create(produto);

                if (Resposta is null)
                    return BadRequest("Categoria não encontrada");

                return CreatedAtAction(nameof(GetById), new { Id = produto.id }, produto);
            }

            [HttpPut]
            public async Task<ActionResult> Update([FromBody] Produto produto)
            {
                if (produto.id == 0)
                    return BadRequest("Id da Produto é invalido");

                var validarProduto = await _produtoValidator.ValidateAsync(produto);

                if (!validarProduto.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validarProduto);
                }

                var Resposta = await _produtoService.Update(produto);

                if (Resposta == null)
                    return NotFound("Produto e/ou TCategoria não Encontrados");

                return Ok(Resposta);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(long id)
            {
                var BuscaProduto = await _produtoService.GetById(id);

                if (BuscaProduto is null)
                    return NotFound("A Produto não foi encontrada!");

                await _produtoService.Delete(BuscaProduto);

                return NoContent();
            }
        
    }
}
