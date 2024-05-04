using FluentValidation;
using FluentValidation.Results;
using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Models.Base;
using LHF.FazendaTech.Business.Notificacoes;

namespace LHF.FazendaTech.Business.Services.Base
{
    public class BaseService
    {
        private readonly INotificador _notificador;
        private readonly IUnitOfWork _uow;

        public BaseService(INotificador notificador, IUnitOfWork uow)
        {
            _notificador = notificador;
            _uow = uow;
        }
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificar(item.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        protected async Task<bool> Commit()
        {
            if (await _uow.Commit()) return true;

            Notificar("Não foi possível salvar os dados no banco!");
            return false;
        }
    }
}

