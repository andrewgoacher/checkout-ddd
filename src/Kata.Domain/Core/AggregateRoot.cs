using System;
using System.Collections.Generic;

namespace Kata.Domain.Core
{
    public abstract class AggregateRoot<TId>
    {
        public TId Id { get; protected set; }

        protected AggregateRoot()
        {
        }
        
        public void Load(IEnumerable<object> history)
        {
            foreach (var change in history)
            {
                OnApply(change);
            }
        }

        protected void Apply(object @event)
        {
            OnApply(@event);
            EnsureValidState();
        }

        protected abstract void OnApply(object @event);
        protected abstract void EnsureValidState();
    }
}