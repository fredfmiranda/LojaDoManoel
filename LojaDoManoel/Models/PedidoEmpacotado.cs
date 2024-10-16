using System.Text.Json.Serialization;

namespace LojaDoManoel.Models
{
    public class PedidoEmpacotado
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("caixas")]
        public List<CaixaEmpacotada> Caixas { get; set; }
    }

    public class PedidoEmpacotadoOutput
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("caixas")]
        public List<CaixaEmpacotadaOutput> Caixas { get; set; }
    }
}
