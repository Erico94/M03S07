using AutoMapper;
using FichaCadastro.Dto.Ficha;
using FichaCadastro.Dto.Telefone;
using FichaCadastro.Model;

namespace FichaCadastro.AutoMapper
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {
            //origem .... destino
            CreateMap<FichaCreateDto, FichaModel>()
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(dados => dados.NomeCompleto))
                .ForMember(destino => destino.Email, origem => origem.MapFrom(dados => dados.EmailInformado.ToLower()))
                .ForMember(destino => destino.DataNascimento, origem => origem.MapFrom(dados => dados.DataDeNascimento));

            //origem .... destino
            CreateMap<FichaModel, FichaReadDto>();

            //origem .... destino
            CreateMap<FichaUpdateDto, FichaModel>();

            //origem .... destino
            CreateMap<FichaModel, FichaReadDto>();

            //origem .... destino
            CreateMap<FichaModel, FichaDetalhesReadDto>()
                .ForMember(destino => destino.Detalhes, origem => origem.MapFrom(dados => dados.DetalheModels));

            //origem .... destino
            CreateMap<DetalheModel, DetalhesReadDto>();

            //origem...destino
            CreateMap<TelefoneModel, TelefoneReadDto>();
            CreateMap<TelefoneCreateDto, TelefoneModel>();
            CreateMap<TelefoneModel, IEnumerable<TelefoneReadDto>>();



        }
    }
}
