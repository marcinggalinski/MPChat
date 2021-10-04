namespace MPChat.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
