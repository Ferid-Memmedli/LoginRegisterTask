using SharedLayer.Utilities.Concrete;

namespace SharedLayer.Utilities.Abstract
{
    public abstract class BaseServiceResult
    {
        protected static IResult<TResult> Success<TResult>(TResult entity = default, string message = null)
        {
            return new Result<TResult>(entity, message);
        }

        protected static IResult<TResult> Error<TResult>(params string[] errors)
        {
            return new Result<TResult>(errors: errors);
        }
    }
}
