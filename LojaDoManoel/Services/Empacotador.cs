using LojaDoManoel.Interfaces;
using LojaDoManoel.Models;

namespace LojaDoManoel.Services
{

    public class Empacotador : IEmpacotador
    {
        public List<CaixaEmpacotada> EmpacotarProdutos(Pedido pedido, List<CaixaDisponivel> caixasDisponiveis)
        {
            if (pedido == null || pedido.Produtos == null || !pedido.Produtos.Any())
            {
                throw new ArgumentException("O pedido não possui produtos válidos.");
            }

            var caixasEmpacotadas = new List<CaixaEmpacotada>();

            // Tentar empacotar todos os produtos
            var caixaAtual = new CaixaEmpacotada
            {
                CaixaId = null,
                Produtos = new List<Produto>()
            };

            foreach (var produto in pedido.Produtos)
            {
                if (produto == null || produto.Dimensoes == null)
                {
                    throw new ArgumentException("Produto ou suas dimensões não podem ser nulos.");
                }

                var caixaDisponivel = EncontrarCaixaDisponivel(produto, caixasDisponiveis);

                if (caixaDisponivel != null)
                {
                    // Produto cabe na caixa, adiciona
                    if (caixaAtual.CaixaId == null)
                    {
                        caixaAtual.CaixaId = caixaDisponivel.CaixaId;
                    }

                    caixaAtual.Produtos.Add(produto);  // Armazenando o objeto Produto completo
                }
                else
                {
                    // Produto não cabe em nenhuma caixa
                    caixasEmpacotadas.Add(new CaixaEmpacotada
                    {
                        CaixaId = null,
                        Produtos = new List<Produto> { produto },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                }
            }

            if (caixaAtual.Produtos.Any())
            {
                caixasEmpacotadas.Add(caixaAtual);
            }

            return caixasEmpacotadas;
        }


        private CaixaDisponivel EncontrarCaixaDisponivel(Produto produto, List<CaixaDisponivel> caixasDisponiveis)
        {
            foreach (var caixa in caixasDisponiveis)
            {
                if (produto.Dimensoes.Altura <= caixa.Altura &&
                    produto.Dimensoes.Largura <= caixa.Largura &&
                    produto.Dimensoes.Comprimento <= caixa.Comprimento)
                {
                    return caixa; 
                }
            }

            return null; 
        }

    }
}

    

