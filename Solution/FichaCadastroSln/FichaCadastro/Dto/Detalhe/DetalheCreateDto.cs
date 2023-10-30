using FichaCadastro.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FichaCadastro.Dto.Detalhe
{
    public class DetalheCreateDto
    {
        [Required] 
        public DateTime DataCadastro { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(500), Required] 
        public string FeedBack { get; set; }

        [Required] 
        public NotasEnum Nota { get; set; }

        [Required] 
        public bool Situacao { get; set; }

        [Required] public int FichaModelId { get; set; }
    }
}
