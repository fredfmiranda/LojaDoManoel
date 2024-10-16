using System.Text.Json.Serialization;

namespace LojaDoManoel.Models
{
    public class CaixaDisponivel : Dimensoes
    {
        public string CaixaId { get; set; }

        [JsonIgnore]
        public Dimensoes EspacoDisponivel { get; set; }
    }
}
