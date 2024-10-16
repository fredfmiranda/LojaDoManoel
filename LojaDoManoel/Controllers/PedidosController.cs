using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LojaDoManoel.Models;
using LojaDoManoel.Interfaces;

namespace LojaDoManoel.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IEmpacotador _empacotador;

        public PedidosController(IEmpacotador empacotador)
        {
            _empacotador = empacotador;
        }

        [HttpPost("processar")]
        public IActionResult ProcessarPedidos([FromBody] PedidoWrapper pedidoWrapper)
        {
            if (pedidoWrapper == null || pedidoWrapper.Pedidos == null || !pedidoWrapper.Pedidos.Any())
            {
                return BadRequest("A lista de pedidos não pode estar vazia ou nula.");
            }

            var resultado = new List<PedidoEmpacotadoOutput>();
            var caixasDisponiveis = ObterCaixasDisponiveis();

            foreach (var pedido in pedidoWrapper.Pedidos)
            {
                if (pedido == null || pedido.Produtos == null || !pedido.Produtos.Any())
                {
                    return BadRequest("Um pedido inválido foi fornecido. Verifique os produtos.");
                }
                var caixasEmpacotadas = _empacotador.EmpacotarProdutos(pedido, caixasDisponiveis);
                var caixasResposta = caixasEmpacotadas.Select(caixa => new CaixaEmpacotadaOutput
                {
                    CaixaId = caixa.CaixaId,
                    Produtos = caixa.Produtos.Select(p => new ProdutoOutput
                    {
                        ProdutoId = p.ProdutoId
                    }).ToList(),
                    Observacao = caixa.Observacao != null ? caixa.Observacao : null
                }).ToList();


                resultado.Add(new PedidoEmpacotadoOutput
                {
                    PedidoId = pedido.PedidoId,
                    Caixas = caixasResposta
                });
            }

            return Ok(resultado);
        }
        private List<CaixaDisponivel> ObterCaixasDisponiveis()
        {
            return new List<CaixaDisponivel>
            {
                new CaixaDisponivel { CaixaId = "Caixa 1", Altura = 50, Largura = 50, Comprimento = 50 },
                new CaixaDisponivel { CaixaId = "Caixa 2", Altura = 40, Largura = 40, Comprimento = 40 },
                new CaixaDisponivel { CaixaId = "Caixa 3", Altura = 60, Largura = 60, Comprimento = 60 }
            };
        }
    }


}
