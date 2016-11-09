namespace SecurityConsultantCore.Common
{
    public interface IFactory<out T>
    {
        T Create();
    }
}