using System.Text.Json.Serialization;
using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using MediatR;

namespace Framework.Core.Messages
{
    public abstract class  Command<TResult> :Message, ICommand<TResult>
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set;}
        [JsonIgnore]
        private ValidationResult _validationResult{get; set;}
        [JsonIgnore]
        private TResult _commandResult {get;set;}

        protected Command() => Timestamp = DateTime.Now;

        public TResult GetCommandOutput() => _commandResult;

        public ValidationResult GetValidationResult() => _validationResult;


        protected void AddValidCommand(ValidationResult validationResult) => _validationResult = validationResult;


        protected void AddCommandOutput(TResult commandOutput) => this._commandResult = commandOutput;
    }
}
