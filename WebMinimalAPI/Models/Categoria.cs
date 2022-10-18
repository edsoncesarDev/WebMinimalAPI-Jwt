using System.Text.Json.Serialization;

namespace WebMinimalAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        [JsonIgnore]
        public ICollection<Produto> Produtos { get; set; }
    }
}
