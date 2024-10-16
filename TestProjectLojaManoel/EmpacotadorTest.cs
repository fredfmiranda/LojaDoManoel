using LojaDoManoel;
using LojaDoManoel.Models;
using LojaDoManoel.Services;

namespace TestProjectLojaManoel
{
    public class EmpacotadorTest
    {
        //Testes básicos para o empacotador
        [Fact]
        public void EmpacotarProdutos_TodosProdutosCabemNaMesmaCaixa_DeveEmpacotarCorretamente()
        {
            var pedido = CriarPedidoComProdutos();

            var caixasDisponiveis = CriarCaixasDisponiveis();
            var empacotador = new Empacotador();

            var resultado = empacotador.EmpacotarProdutos(pedido, caixasDisponiveis);

            Assert.Single(resultado);
            Assert.Equal("Caixa 1", resultado[0].CaixaId);
            Assert.Equal(2, resultado[0].Produtos.Count);
        }

        [Fact]
        public void EmpacotarProdutos_ProdutoNaoCabe_DeveRetornarObservacao()
        {
        
            var pedido = CriarPedidoComProdutosEObservacao();

            var caixasDisponiveis = CriarCaixasDisponiveis();
            var empacotador = new Empacotador();
            
            var resultado = empacotador.EmpacotarProdutos(pedido, caixasDisponiveis);
            
            Assert.Single(resultado);
            Assert.Null(resultado[0].CaixaId);
            Assert.Equal("Produto não cabe em nenhuma caixa disponível.", resultado[0].Observacao);
        }

        private Pedido CriarPedidoComProdutos()
        {
            return new Pedido
            {
                PedidoId = 1,
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        ProdutoId = "PS5",
                        Dimensoes = new Dimensoes { Altura = 40, Largura = 10, Comprimento = 25 }
                    },
                    new Produto
                    {
                        ProdutoId = "Volante",
                        Dimensoes = new Dimensoes { Altura = 40, Largura = 30, Comprimento = 30 }
                    }
                }
            };
        }

        private Pedido CriarPedidoComProdutosEObservacao()
        {
            return new Pedido
            {
                PedidoId = 1,
                Produtos = new List<Produto>
                {
                    new Produto
                    {
                        ProdutoId = "Cadeira Gamer",
                        Dimensoes = new Dimensoes { Altura = 120, Largura = 60, Comprimento = 70 }
                    }
                }
            };
        }

        private List<CaixaDisponivel> CriarCaixasDisponiveis()
        {
            return new List<CaixaDisponivel>
            {
                new CaixaDisponivel
                {
                    CaixaId = "Caixa 1",
                    Altura = 50,
                    Largura = 40,
                    Comprimento = 30
                },
                new CaixaDisponivel
                {
                    CaixaId = "Caixa 2",
                    Altura = 70,
                    Largura = 50,
                    Comprimento = 40
                }
            };
        }




    }
}