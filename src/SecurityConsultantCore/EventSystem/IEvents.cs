using System;

namespace SecurityConsultantCore.EventSystem
{
    public interface IEvents
    {
        void Subscribe<T>(Action<T> onEvent);
        void Publish<T>(T payload);
    }
}
