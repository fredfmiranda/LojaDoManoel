using LojaDoManoel.Models;

namespace LojaDoManoel.Interfaces
{
    public interface IEmpacotador
    {
        List<CaixaEmpacotada> EmpacotarProdutos(Pedido pedido, List<CaixaDisponivel> caixasDisponiveis);
    }

}
