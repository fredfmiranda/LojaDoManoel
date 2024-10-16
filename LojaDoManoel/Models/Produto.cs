using System.Text.Json.Serialization;

namespace LojaDoManoel.Models
{
    public class Produto
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; }
        [JsonPropertyName("dimensoes")]

        public Dimensoes Dimensoes { get; set; }
    }

    public class ProdutoOutput
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; }
    }

}
