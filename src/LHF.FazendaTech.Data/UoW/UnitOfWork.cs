using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Data.Context;

namespace LHF.FazendaTech.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FazendaTechContext _context;

        public UnitOfWork(FazendaTechContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
