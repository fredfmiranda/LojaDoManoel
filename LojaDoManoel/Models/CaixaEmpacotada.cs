using System.Text.Json.Serialization;

namespace LojaDoManoel.Models
{
    public class CaixaEmpacotada
    {
        [JsonPropertyName("caixa_id")]
        public string CaixaId { get; set; }

        [JsonPropertyName("produtos")]
        public List<Produto> Produtos { get; set; } 

        [JsonPropertyName("observacao")]
        public string Observacao { get; set; }

        [JsonIgnore] 
        public Dimensoes EspacoDisponivel { get; set; }
    }

    public class CaixaEmpacotadaOutput
    {
        [JsonPropertyName("caixa_id")]
        public string CaixaId { get; set; }

        [JsonPropertyName("produtos")]
        public List<ProdutoOutput> Produtos { get; set; }  // Lista de ProdutoOutput na resposta

        [JsonPropertyName("observacao")]
        public string Observacao { get; set; }
    }




}
