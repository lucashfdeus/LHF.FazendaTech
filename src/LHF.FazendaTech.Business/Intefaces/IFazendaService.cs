using LHF.FazendaTech.Business.Models;

namespace LHF.FazendaTech.Business.Intefaces
{
    public interface IFazendaService
    {
        Task Adicionar(Fazenda fornecedor);
        Task Atualizar(Fazenda fornecedor);
        Task Remover(Guid id);
    }
}
