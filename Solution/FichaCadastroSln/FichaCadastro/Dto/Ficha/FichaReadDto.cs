using FichaCadastro.Enumerators;
using FichaCadastro.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FichaCadastro.Dto.Ficha
{
    public class FichaReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class FichaDetalhesReadDto : FichaReadDto
    {
        public List<DetalhesReadDto>? Detalhes { get; set; }
    }

    public class DetalhesReadDto
    {
        public int Id { get; set; }
        public string FeedBack { get; set; }
        public NotasEnum Nota { get; set; }
        //public bool Situacao { get; set; }
        //public FichaModel Ficha { get; set; }
    }

}
