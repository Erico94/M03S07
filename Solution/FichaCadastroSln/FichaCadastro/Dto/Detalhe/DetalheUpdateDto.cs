using FichaCadastro.Enumerators;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FichaCadastro.Dto.Detalhe
{
    public class DetalheUpdateDto
    {
        [Column(TypeName = "VARCHAR"), StringLength(500), Required]
        public string FeedBack { get; set; }

        [Required]
        public NotasEnum Nota { get; set; }

        [Required]
        public bool Situacao { get; set; }

        [Required] public int FichaModelId { get; set; }
    }
}
