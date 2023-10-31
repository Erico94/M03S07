using AutoMapper;
using FichaCadastro.Dto.Telefone;
using FichaCadastro.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace FichaCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        public readonly FichaCadastroContextDB _dbContext;
        private readonly IMapper _mapper;


        public TelefoneController(FichaCadastroContextDB fichaCadastroContextDB, IMapper mapper)
        {
            _dbContext = fichaCadastroContextDB;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<TelefoneReadDto> Post([FromBody] TelefoneCreateDto telefoneCreate)
        {
            TelefoneModel telefoneModel = _mapper.Map<TelefoneModel>(telefoneCreate);
            _dbContext.Add(telefoneModel);
            _dbContext.SaveChanges();
            TelefoneReadDto telefoneRead = _mapper.Map<TelefoneReadDto>(_dbContext.Telefones
                .Where(w => w.FichaModelId == telefoneCreate.FichaModelId));
            return Created("Criado com sucesso", telefoneRead);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TelefoneReadDto>> Get([FromQuery] int? telefoneId)
        {
            try
            {
                if (telefoneId.HasValue)
                {
                    IEnumerable<TelefoneReadDto> telefone = _mapper.Map<IEnumerable<TelefoneReadDto>>
                        (_dbContext.Telefones.Where(w => w.Id == telefoneId.Value));
                    return Ok(telefone);
                }
                IEnumerable<TelefoneReadDto> telefones = _mapper.Map<IEnumerable<TelefoneReadDto>>
                    (_dbContext.Telefones.ToList());
                return Ok(telefones);
            }
            catch (Exception)
            {
                return BadRequest("Erro interno");
            }
        }
    }
}
