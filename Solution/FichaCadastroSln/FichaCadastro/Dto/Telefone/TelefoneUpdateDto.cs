using System.ComponentModel.DataAnnotations;

namespace FichaCadastro.Dto.Telefone
{
    public class TelefoneUpdateDto
    {
        [Required]
        public int Ddd { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public int FichaModelId { get; set; }
    }
}
