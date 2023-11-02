using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FichaCadastro.Base;

namespace FichaCadastro.Model
{
    [Table("Ficha")]
    public class FichaModel : RelationalBase
    {
        [Column (TypeName = "VARCHAR"), StringLength(250), Required] public string Nome { get; set; }
        [Column (TypeName = "VARCHAR"), StringLength(100), Required] public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public TelefoneModel Telefone { get; set; }
        public Collection<DetalheModel> DetalheModels { get; set; } 
    }
}
