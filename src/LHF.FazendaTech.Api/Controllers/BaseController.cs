using LHF.FazendaTech.Business.Intefaces;
using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace LHF.FazendaTech.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;
        public readonly IAppIdentityUser AppUser;

        //A INTERFACE IUser PODE SER INJETADA EM QUALQUER CONSTRUTOR DA APLICAÇÃO
        //PARA RECUPERAR OS DADOS DOS USUÁRIOS.

        protected Guid UserId { get; set; } = Guid.Empty;
        protected string UserName { get; set; } = string.Empty;
        protected bool UsuarioAutenticado { get; set; }


        protected BaseController(INotificador notificador,
                                 IAppIdentityUser appUser)
        {
            _notificador = notificador;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                UserName = appUser.GetUsername();
                UsuarioAutenticado = true;
            }
        }
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
