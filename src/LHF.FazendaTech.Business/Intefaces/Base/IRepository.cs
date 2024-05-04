using LHF.FazendaTech.Business.Models.Base;
using System.Linq.Expressions;

namespace LHF.FazendaTech.Business.Intefaces.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);

        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Remover(Guid id);
       
        Task<int> SaveChanges();
    }
}
