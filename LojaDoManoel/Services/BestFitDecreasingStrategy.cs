using LojaDoManoel.Interfaces;
using LojaDoManoel.Models;
using System;
using System.Collections.Generic;

public class BestFitDecreasingStrategyHeap : IPackingStrategy
{
    public List<CaixaEmpacotada> Empacotar(List<Produto> produtos, List<CaixaDisponivel> caixasDisponiveis)
    {
        var caixasUsadas = new List<CaixaEmpacotada>();

        var produtosOrdenados = produtos
            .OrderByDescending(p => p.Dimensoes.Volume)
            .ToList();

        //Heap Binário (Min-Heap ordenada pelo espaço restante nas caixas)
        var caixasHeap = new SortedSet<CaixaEmpacotada>(new ComparadorCaixa());

        foreach (var produto in produtosOrdenados)
        {
            // Busca a caixa com o menor espaço restante que ainda pode acomodar o produto
            var melhorCaixa = caixasHeap
                .FirstOrDefault(caixa => CabeProdutoNaCaixa(produto.Dimensoes, caixa.EspacoDisponivel));

            if (melhorCaixa != null)
            {
                caixasHeap.Remove(melhorCaixa); 
                melhorCaixa.Produtos.Add(produto);
                AtualizarEspacoCaixa(melhorCaixa, produto.Dimensoes);
                caixasHeap.Add(melhorCaixa); 
            }
            else
            {
                // Tenta encontrar uma nova caixa disponível
                var caixaDisponivel = caixasDisponiveis
                    .FirstOrDefault(caixa => CabeProdutoNaCaixa(produto.Dimensoes, caixa.EspacoDisponivel));

                if (caixaDisponivel != null)
                {
                    var novaCaixa = new CaixaEmpacotada
                    {
                        CaixaId = caixaDisponivel.CaixaId,
                        Produtos = new List<Produto> { produto },
                        EspacoDisponivel = new Dimensoes
                        {
                            Altura = caixaDisponivel.EspacoDisponivel.Altura - produto.Dimensoes.Altura,
                            Largura = caixaDisponivel.EspacoDisponivel.Largura - produto.Dimensoes.Largura,
                            Comprimento = caixaDisponivel.EspacoDisponivel.Comprimento - produto.Dimensoes.Comprimento
                        }
                    };
                    caixasUsadas.Add(novaCaixa);
                    caixasHeap.Add(novaCaixa); 
                }
                else
                {
                    // Produto não cabe em nenhuma caixa disponível
                    caixasUsadas.Add(new CaixaEmpacotada
                    {
                        CaixaId = null,
                        Produtos = new List<Produto> { produto },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                }
            }
        }

        return caixasUsadas;
    }

    private bool CabeProdutoNaCaixa(Dimensoes produto, Dimensoes caixa)
    {
        return produto.Altura <= caixa.Altura &&
               produto.Largura <= caixa.Largura &&
               produto.Comprimento <= caixa.Comprimento;
    }

    private void AtualizarEspacoCaixa(CaixaEmpacotada caixa, Dimensoes produto)
    {
        caixa.EspacoDisponivel.Altura -= produto.Altura;
        caixa.EspacoDisponivel.Largura -= produto.Largura;
        caixa.EspacoDisponivel.Comprimento -= produto.Comprimento;
    }
}

public class ComparadorCaixa : IComparer<CaixaEmpacotada>
{
    public int Compare(CaixaEmpacotada x, CaixaEmpacotada y)
    {
        int volumeX = x.EspacoDisponivel.Altura * x.EspacoDisponivel.Largura * x.EspacoDisponivel.Comprimento;
        int volumeY = y.EspacoDisponivel.Altura * y.EspacoDisponivel.Largura * y.EspacoDisponivel.Comprimento;

        return volumeX.CompareTo(volumeY); 
    }
}
