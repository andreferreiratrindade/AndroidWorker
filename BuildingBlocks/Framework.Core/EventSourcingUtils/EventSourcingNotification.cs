using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.EventSourcingUtils
{
    public abstract class EventSourcingNotification : INotification
    {
        public bool IsReplayingEvent { get; set; }
        public bool ShouldSerializeIsReplayingEvent() => false;
    }
}
