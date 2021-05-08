using System;

namespace Kata.Domain.Core
{
    public abstract class Event<TId>
    {
        private readonly Action<object> _applyFunc;

        protected Event(Action<object> applyFunc)
        {
            _applyFunc = applyFunc;
        }

        public TId Id { get; protected set; }

        protected void Apply(object @event)
        {
            OnApply(@event);
            _applyFunc(@event);
        }

        protected abstract void OnApply(object @event);
    }
}