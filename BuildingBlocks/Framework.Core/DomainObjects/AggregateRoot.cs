using System;
using System.Collections.Generic;
using FluentValidation;
using Framework.Core.Messages;

namespace Framework.Core.DomainObjects
{
    public abstract class AggregateRoot: IAggregateRoot
    {

        readonly ICollection<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();
        public Guid AggregateId { get; protected set; } = Guid.Empty;
        private long _version  = -1;

        public long GetVersion() => _version;

        public void SetVersion(long version) {
            _version = version;
        }

        public void MarkChangesAsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        protected abstract void When(IDomainEvent @event);

        public void RaiseEvent(IDomainEvent @event)
        {
            When(@event);
            _uncommittedEvents.Add(@event);
        }

        public void LoadFromHistory(long version, IEnumerable<IDomainEvent> history)
        {
            SetVersion(version);

            foreach (var @event in history)
            {
                When(@event);
            }
        }

        public IEnumerable<IDomainEvent> GetUncommittedChanges() => _uncommittedEvents;


        //public void LoadsFromHistory(IEnumerable<Event> history)
        //{
        //    foreach (var e in history) ApplyChangeHistory(e);
        //}

        //private void ApplyChangeHistory(Event @event)
        //{
        //    this.AsDynamic().Apply(@event);

        //}

        //public void LoadFromHistory(long version, IEnumerable<IDomainEvent> history)
        //{
        //    Version = version;

        //    foreach (var @event in history)
        //    {
        //        When(@event);
        //    }
        //}

        //public void ApplyChange(Event @event)
        //{
        //    AddEvent(@event);
        //    var apply = GetType().GetMethod("Apply", new[] { @event.GetType() });

        //    if (apply == null)
        //        throw new Exception("Error apply change exceptions");

        //    apply.Invoke(this, new object[] { @event });
        //}

        //protected void ApplyChange(Event @event)
        //{
        //    this.AsDynamic().Apply(@event);

        //    AddEvent(@event);
        //}
        /*
        /// <summary>
        /// Represents the state (data) of an aggregate. A derived class should be a POCO
        /// (DTO/Packet) that includes a When method for each event type that changes its
        /// property values. Ideally, the property values for an instance of  this class 
        /// should be modified only through its When methods.
        /// </summary>
        public abstract class AggregateState
        {
            public void Apply(IEvent @event)
            {
                var when = GetType().GetMethod("When", new[] { @event.GetType() });

                if (when == null)
                    throw new MethodNotFoundException(GetType(), "When", @event.GetType());

                when.Invoke(this, new object[] { @event });
            }
        }
        */



    }
}