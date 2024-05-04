using LHF.FazendaTech.Business.Intefaces;
using LHF.FazendaTech.Business.Models;
using LHF.FazendaTech.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LHF.FazendaTech.Data.Repository
{
    public class FazendaRepository : Repository<Fazenda>, IFazendaRepository
    {
        public FazendaRepository(FazendaTechContext context) : base(context) { }

        public async Task<Fazenda> ObterFazendaEndereco(Guid id)
        {
            var fazendaEndereco = await _context.Fazendas.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(e => e.Id == id);

            return fazendaEndereco;
        }

        public async Task<Endereco> ObterEnderecoPorFazenda(Guid fazendaId)
        {
            var enderecoFazenda = await _context.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FazendaId == fazendaId);

            return enderecoFazenda;
        }

        public void RemoverEnderecoFazenda(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
        }
    }
}
