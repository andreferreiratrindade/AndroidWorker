using System.Text.Json.Serialization;
using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Grpc.Core;

using MediatR;

namespace Framework.Core.Messages
{
    public abstract class  Command<TResult> :Message, ICommand<TResult>
    {

        [JsonIgnore]
        public DateTime Timestamp { get; private set;} = DateTime.Now;
        [JsonIgnore]
        private ValidationResult _validationResult{get; set;}
        [JsonIgnore]
        private TResult _commandResult {get;set;} = Activator.CreateInstance<TResult>();
        [JsonIgnore]
        private RollBackEvent _rollBackEvent {get;set;}
        [JsonIgnore]
        public CorrelationIdGuid CorrelationId {get;}

        public TResult GetCommandOutput() => _commandResult;

        public ValidationResult GetValidationResult() => _validationResult;

        public RollBackEvent GetRollBackEvent() => _rollBackEvent;

        protected void AddValidCommand(ValidationResult validationResult) => _validationResult = validationResult;

        protected void AddCommandOutput(TResult commandOutput) => this._commandResult = commandOutput;

        protected void AddRollBackEvent(RollBackEvent rollBackEvent) => this._rollBackEvent = rollBackEvent;

        [JsonIgnore]
        public string MessageType {get;private set;}
       public Command(CorrelationIdGuid correlationId)
       {
        this.CorrelationId = correlationId;
           MessageType =  GetType().Name;
       }
    }
}
