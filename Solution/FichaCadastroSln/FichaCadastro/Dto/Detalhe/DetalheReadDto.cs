using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FichaCadastro.Enumerators;
using FichaCadastro.Model;

namespace FichaCadastro.Dto.Detalhe
{
    public class DetalheReadDto
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string FeedBack { get; set; }
        public NotasEnum Nota { get; set; }
        public bool Situacao { get; set; }
        public int FichaModelId { get; set; }
        public FichaModel Ficha { get; set; }
    }
}
