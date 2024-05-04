using LHF.FazendaTech.Business.Intefaces;
using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Models;
using LHF.FazendaTech.Business.Models.Validations;
using LHF.FazendaTech.Business.Services.Base;

namespace LHF.FazendaTech.Business.Services
{
    public class FazendaService : BaseService, IFazendaService
    {
        private readonly IFazendaRepository _fazendaRepository;

        public FazendaService(INotificador notificador,
                              IUnitOfWork uow,
                              IFazendaRepository fazendaRepository) : base(notificador, uow)
        {
            _fazendaRepository = fazendaRepository;
        }

        public async Task Adicionar(Fazenda fazenda)
        {
            if (!ExecutarValidacao(new FazendaValidation(), fazenda)
                           || !ExecutarValidacao(new EnderecoValidation(), fazenda.Endereco)) return;

            if (_fazendaRepository.Buscar(f => f.Documento == fazenda.Documento).Result.Any())
            {
                Notificar("Já existe uma fazenda com este documento infomado.");
                return;
            }

            _fazendaRepository.Adicionar(fazenda);
            await Commit();
        }

        public async Task Atualizar(Fazenda fazenda)
        {
            if (!ExecutarValidacao(new FazendaValidation(), fazenda)) return;

            if (_fazendaRepository.Buscar(f => f.Documento == fazenda.Documento && f.Id != fazenda.Id).Result.Any())
            {
                Notificar("Já existe um fazenda com este documento infomado.");
                return;
            }

            _fazendaRepository.Atualizar(fazenda);
            await Commit();
        }       

        public async Task Remover(Guid id)
        {
            var fazenda = await _fazendaRepository.ObterFazendaEndereco(id);

            if (fazenda == null)
            {
                Notificar("Fornecedor não existe!");
                return;
            }            

            var endereco = await _fazendaRepository.ObterEnderecoPorFazenda(id);

            if (endereco != null)
            {
                _fazendaRepository.RemoverEnderecoFazenda(endereco);
            }

            _fazendaRepository.Remover(id);

            await Commit();
        }
    }
}
