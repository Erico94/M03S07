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

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Put(int id, [FromBody] FichaUpdateDto fichaUpdateDto)
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
    }
}
