namespace WebApi.Data.CommonInterface
{
    public interface BaseInterface<T> where T:class
    {
        void Add(T model);
        void Update(T model);
        void Remove(T model);
    }
}
