using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata.Domain.Core
{
    public abstract class AggregateRoot
    {
        private readonly List<object> _changes;
        
        public Guid Id { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<object>();
        }

        public void ClearAllChanges() => _changes.Clear();
        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

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
            _changes.Add(@event);
        }

        protected abstract void OnApply(object @event);
        protected abstract void EnsureValidState();
    }
}