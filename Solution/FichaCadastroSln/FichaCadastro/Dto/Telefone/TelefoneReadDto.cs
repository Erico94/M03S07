using FichaCadastro.Model;
using System.ComponentModel.DataAnnotations;

namespace FichaCadastro.Dto.Telefone
{
    public class TelefoneReadDto
    {
        public int Id { get; set; }

        public int Ddd { get; set; }

        public string Numero { get; set; }

        public bool Ativo { get; set; }

        public int FichaModelId { get; set; }

        public FichaModel Ficha { get; set; }
    }
}
