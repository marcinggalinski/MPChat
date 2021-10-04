namespace MPChat.DataAccess.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T Get(int id);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
