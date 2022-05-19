namespace Core.Model
{
    public interface IRepository<T>
    {
        public T GetById(Guid id);
        public List<T> GetRange(int range = 20);
        public void Insert(T entity);
        public void Update(Guid id, T data);
        public void Delete(Guid id);
    }
}