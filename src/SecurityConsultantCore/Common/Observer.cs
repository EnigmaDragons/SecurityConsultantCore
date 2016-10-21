namespace SecurityConsultantCore.Common
{
    public interface Observer<T>
    {
        void Update(T obj);
    }
}
