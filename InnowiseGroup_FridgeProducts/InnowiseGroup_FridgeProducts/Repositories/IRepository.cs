namespace FridgeProducts.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T GetModel(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();
    }
}
