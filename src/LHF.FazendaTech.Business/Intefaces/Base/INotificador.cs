using LHF.FazendaTech.Business.Notificacoes;

namespace LHF.FazendaTech.Business.Intefaces.Base
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
