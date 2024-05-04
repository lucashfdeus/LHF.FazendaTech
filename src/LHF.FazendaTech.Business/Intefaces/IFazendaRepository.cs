using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Models;

namespace LHF.FazendaTech.Business.Intefaces
{
    public interface IFazendaRepository : IRepository<Fazenda>
    {
        Task<Fazenda> ObterFazendaEndereco(Guid id);
        Task<Endereco> ObterEnderecoPorFazenda(Guid fazendaId);
        void RemoverEnderecoFazenda(Endereco endereco);
    }
}
