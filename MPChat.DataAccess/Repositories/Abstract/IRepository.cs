namespace MPChat.DataAccess.Repositories.Abstract
{
    public interface IRepository<T>
        where T : class
    {
        T GetById(int id);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
