using AutoMapper;
using FichaCadastro.AutoMapper;
using FichaCadastro.Dto.Ficha;
using FichaCadastro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FichaCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FichaCadastroContextDB _fichaCadastroDbContext;
        private readonly ILogger<FichaController> _logger;

        public FichaController(IMapper mapper, FichaCadastroContextDB fichaCadastro, ILogger<FichaController> logger)
        {
            _mapper = mapper;
            _fichaCadastroDbContext = fichaCadastro;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<FichaReadDto> Post([FromBody] FichaCreateDto fichaCreateDto)
        {
            try
            {
                var verificaSeExisteEmail = _fichaCadastroDbContext.FichaModels
                    .FirstOrDefault(w => w.Email == fichaCreateDto.EmailInformado);

                if (verificaSeExisteEmail != null)
                {
                    return StatusCode(HttpStatusCode.Conflict.GetHashCode());
                }

                FichaModel fichaModel = _mapper.Map<FichaModel>(fichaCreateDto);
                _fichaCadastroDbContext.Add(fichaModel);
                _fichaCadastroDbContext.SaveChanges();

                FichaReadDto fichaReadDto = _mapper.Map<FichaReadDto>(fichaModel);


                return StatusCode(HttpStatusCode.Created.GetHashCode(), fichaReadDto);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Put([FromRoute] int id, [FromBody] FichaUpdateDto fichaUpdateDto)
        {
            try
            {
                FichaModel? fichaModel = _fichaCadastroDbContext.FichaModels.Find(id);

                if (fichaModel != null)
                {
                    fichaModel = _mapper.Map(fichaUpdateDto, fichaModel);

                    _fichaCadastroDbContext.FichaModels.Update(fichaModel);
                    _fichaCadastroDbContext.SaveChanges();

                    FichaReadDto fichaReadDto = _mapper.Map<FichaReadDto>(fichaModel);

                    return StatusCode(HttpStatusCode.OK.GetHashCode());
                }
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Não pode alterar um registro que não exista no banco de dados.");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                FichaModel? fichaModel = _fichaCadastroDbContext.FichaModels.Include(i => i.DetalheModels).Where(w => w.Id == id).FirstOrDefault();

                if (fichaModel == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Registro não encontrado.");
                }

                if (fichaModel != null && fichaModel.DetalheModels.Count > 0)
                {
                    return StatusCode(HttpStatusCode.Conflict.GetHashCode(), "Não é possivel remover porque contém detalhes.");
                }

                _fichaCadastroDbContext.FichaModels.Remove(fichaModel);
                _fichaCadastroDbContext.SaveChanges();


                return StatusCode(HttpStatusCode.OK.GetHashCode(), "Registro removido com sucesso.");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex);
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FichaDetalhesReadDto>> Get([FromQuery] string? email)
        {
            try
            {
                List<FichaModel> fichaModels;

                if (email == null || email == "")
                {

                    fichaModels = _fichaCadastroDbContext.FichaModels.Include(i => i.DetalheModels).ToList();
                }
                else
                {
                    fichaModels = _fichaCadastroDbContext.FichaModels.Where(w => w.Email == email).ToList();
                }
                IEnumerable<FichaDetalhesReadDto> fichaDetalhesReadDto = _mapper.Map<IEnumerable<FichaDetalhesReadDto>>(fichaModels);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), fichaDetalhesReadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FichaDetalhesReadDto> Get([FromRoute] int id)
        {
            try
            {
                if(id == 0)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Registro não pode ser zero!.");
                }
                FichaModel? fichaModel = _fichaCadastroDbContext.FichaModels.Include(i => i.DetalheModels)
                    .Where(w => w.Id == id).FirstOrDefault();

                if (fichaModel == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Registro não encontrado.");
                }

                FichaDetalhesReadDto fichaDetalhesReaDto = _mapper.Map<FichaDetalhesReadDto>(fichaModel);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), fichaDetalhesReaDto);

                //if (fichaModel != null && fichaModel.DetalheModels.Count > 0)
                //{
                //    return StatusCode(HttpStatusCode.Conflict.GetHashCode(), "Não é possivel remover porque contém detalhes.");
                //}

                //_fichaCadastroDbContext.FichaModels.Remove(fichaModel);
                //_fichaCadastroDbContext.SaveChanges();


                //return StatusCode(HttpStatusCode.OK.GetHashCode(), "Registro removido com sucesso.");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), ex);
            }

        }


    }
}
