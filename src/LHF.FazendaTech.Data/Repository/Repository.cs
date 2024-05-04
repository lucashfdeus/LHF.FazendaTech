using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Models.Base;
using LHF.FazendaTech.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LHF.FazendaTech.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly FazendaTechContext _context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(FazendaTechContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
