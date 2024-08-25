using FluentValidation.Results;
namespace Framework.Core.DomainObjects
{
    public class ReturnOperation<TResult>
    {
        public ValidationResult ValidationResult { get; set; }
        public TResult tResult { get; set; }

        public ReturnOperation(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }


        public ReturnOperation(TResult result)
        {
            tResult = result;
        }

        public bool IsSuccess()
        {
            return ValidationResult == null || ValidationResult.IsValid;
        }



    }
}
