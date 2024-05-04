using AutoMapper;
using LHF.FazendaTech.Api.Controllers;
using LHF.FazendaTech.Api.ViewModels;
using LHF.FazendaTech.Business.Intefaces;
using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LHF.FazendaTech.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fazendas")]
    public class FazendaController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IFazendaRepository _fazendaRepository;
        private readonly IFazendaService _fazendaService;

        public FazendaController(INotificador notificador, IAppIdentityUser user, IMapper mapper, IFazendaRepository fazendaRepository, IFazendaService fazendaService) : base(notificador, user)
        {
            _mapper = mapper;
            _fazendaRepository = fazendaRepository;
            _fazendaService = fazendaService;
        }

        [HttpGet]
        public async Task<IEnumerable<FazendaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FazendaViewModel>>(await _fazendaRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FazendaViewModel>> ObterPorId(Guid id)
        {
            var fazenda = await ObterPorId(id);

            if (fazenda == null) return NotFound();

            return fazenda;
        }

        [HttpPost]
        public async Task<ActionResult<FazendaViewModel>> Adicionar(FazendaViewModel fazendaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fazendaService.Adicionar(_mapper.Map<Fazenda>(fazendaViewModel));

            return CustomResponse(HttpStatusCode.Created, fazendaViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FazendaViewModel>> Atualizar(Guid id, FazendaViewModel fazendaViewModel)
        {
            if (id != fazendaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fazendaService.Atualizar(_mapper.Map<Fazenda>(fazendaViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FazendaViewModel>> Excluir(Guid id)
        {
            await _fazendaService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
