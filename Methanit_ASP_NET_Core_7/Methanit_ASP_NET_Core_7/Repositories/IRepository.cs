namespace Methanit_ASP_NET_Core_7.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Guid id) { return null; }
        T GetModel(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();
    }
}
