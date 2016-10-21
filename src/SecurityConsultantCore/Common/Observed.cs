namespace SecurityConsultantCore.Common
{
    public interface Observed<T>
    {
        void Subscribe(Observer<T> observer);
        void Unsubscribe(Observer<T> observer);
    }
}
