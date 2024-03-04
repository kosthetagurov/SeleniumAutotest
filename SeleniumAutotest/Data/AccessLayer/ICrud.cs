namespace SeleniumAutotest.Data.AccessLayer
{
    public interface ICrud<T, TId>
        where T : DbModelBase<TId>
    {
        T Create(T model);
        void Update(T model);
        List<T> Get();
        List<T> Get(Func<T, bool> predicate);
        T GetById(TId Id);
        void Delete(T model);
    }
}
