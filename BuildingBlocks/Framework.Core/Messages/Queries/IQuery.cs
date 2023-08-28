using MediatR;

namespace Framework.Core.Messages{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}