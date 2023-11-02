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
            telefoneModel = _dbContext.Telefones.Where(w => w.FichaModelId == telefoneCreate.FichaModelId).FirstOrDefault();
            TelefoneReadDto telefoneRead = _mapper.Map<TelefoneReadDto>(telefoneModel);
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

        [HttpPut("{id}")]
        public ActionResult<TelefoneReadDto> Put([FromRoute] int id, [FromBody] TelefoneUpdateDto telefoneUpdate)
        {
            try
            {
                TelefoneModel telefoneModel = _dbContext.Telefones.Where(w => w.Id == id).FirstOrDefault();
                if (telefoneModel == null)
                {
                    return NoContent();
                }
                telefoneModel = _mapper.Map<TelefoneModel>(telefoneUpdate);
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(telefoneModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                TelefoneReadDto telefoneRead = _mapper.Map<TelefoneReadDto>(telefoneModel);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Erro interno");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                TelefoneModel telefone = _dbContext.Telefones.Where(w => w.Id == id).FirstOrDefault();
                if (telefone != null)
                {
                    _dbContext.Remove(telefone);
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro interno");
            }
        }
    }
}
