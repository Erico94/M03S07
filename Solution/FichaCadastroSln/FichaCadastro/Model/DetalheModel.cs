using FichaCadastro.Base;
using FichaCadastro.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FichaCadastro.Model
{
        [Table("Detalhe")]
    public class DetalheModel : RelationalBase
    {
        [Column (TypeName = "VARCHAR"), StringLength(500), Required] public string FeedBack { get; set; }
        [Required] public NotasEnum Nota { get; set; }
        [Required] public bool Situacao { get; set; }
        [Required] public FichaModel Ficha { get; set; }
    }
}
