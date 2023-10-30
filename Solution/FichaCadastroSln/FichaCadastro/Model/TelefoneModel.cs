using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FichaCadastro.Model
{
    public class TelefoneModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Ddd { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public int FichaModelId { get; set; }

        public FichaModel Ficha { get; set; }
    }
}
