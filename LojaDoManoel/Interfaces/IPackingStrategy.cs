using LojaDoManoel.Models;

namespace LojaDoManoel.Interfaces
{
    public interface IPackingStrategy
    {
        List<CaixaEmpacotada> Empacotar(List<Produto> produtos, List<CaixaDisponivel> caixasDisponiveis);
    }
}
