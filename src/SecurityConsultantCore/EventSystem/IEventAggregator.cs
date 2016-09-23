using System;

namespace SecurityConsultantCore.EventSystem
{
    public interface IEventAggregator
    {
        void Subscribe<T>(Action<T> onEvent);
        void Publish<T>(T payload);
    }
}
