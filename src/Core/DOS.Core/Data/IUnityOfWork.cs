namespace DOS.Core.Data
{
    public interface IUnityOfWork
    {
        Task<bool> Commit();
    }
}
