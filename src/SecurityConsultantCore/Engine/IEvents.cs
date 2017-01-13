using System;

namespace SecurityConsultantCore.Engine
{
    public interface IEvents
    {
        void Subscribe<T>(Action<T> onEvent);
        void Publish<T>(T payload);
    }
}
