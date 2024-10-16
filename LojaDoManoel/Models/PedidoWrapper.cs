using System.Text.Json.Serialization;

namespace LojaDoManoel.Models
{
    public class PedidoWrapper
    {
        [JsonPropertyName("pedidos")]
        public List<Pedido> Pedidos { get; set; }
    }
}
