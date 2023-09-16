using AutoMapper;
using FichaCadastro.Dto.Ficha;
using FichaCadastro.Model;

namespace FichaCadastro.AutoMapper
{
    public class ConfigurationMapper : Profile
    {
         public ConfigurationMapper() 
        {
            CreateMap<FichaCreateDto, FichaModel>()
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(dados => dados.NomeCompleto))
                .ForMember(destino => destino.Email, origem => origem.MapFrom(dados => dados.EmailInformado.ToLower()))
                .ForMember(destino => destino.DataNascimento, origem => origem.MapFrom(dados => dados.DataDeNascimento));

            CreateMap<FichaModel, FichaReadDto>();

            CreateMap<FichaUpdateDto, FichaModel>();

        }
    }
}
