namespace LHF.FazendaTech.Business.Intefaces.Base
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
