


namespace Framework.Core.Messages
{

    public class ValidationResultBase{
        public ValidationResultBase()
        {
            Erros = new List<string>();
        }
        public List<string> Erros {get;set;}
    }

    public abstract class ResultBase{

        protected ValidationResultBase ValidationResultBase = new();

        public ValidationResultBase GetValidationResultBase(){
            return ValidationResultBase;
        }

        public List<string> GetErroMessagens(){
            return ValidationResultBase.Erros;
        }

        public void Fail(string message){
            ValidationResultBase.Erros.Add( message);
        }

        public void Fail(List<string> message){
            ValidationResultBase.Erros.AddRange( message);
        }

        public bool IsValid(){
            return !ValidationResultBase.Erros.Any();
        }
    }
    public class Result<T>: ResultBase where T : class
    {   
        public Result(){
            
        }

         public Result(ValidationResultBase validationResultBase)
        {
           this.ValidationResultBase = validationResultBase;
        }
        public Result(Result result)
        {
           this.ValidationResultBase = result.GetValidationResultBase();
        }
        public T? Data {get;set;}
    }

    public class Result : ResultBase
    {   
        public Result(){
            
        }

         public Result(ValidationResultBase validationResultBase)
        {
           this.ValidationResultBase = validationResultBase;
        }
        public Result(Result result)
        {
           this.ValidationResultBase = result.GetValidationResultBase();
        }
    }
}