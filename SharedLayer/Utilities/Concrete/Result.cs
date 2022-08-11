using SharedLayer.Utilities.Abstract;

namespace SharedLayer.Utilities.Concrete
{
    public class Result<T> : IResult<T>
    {
        public Result(T data = default, string message = null)
        {
            Data = data;
            Message = message;
            Errors = new List<string>();
        }

        public Result(T data = default, string message = null, params string[] errors)
        {
            Data = data;
            Message = message;
            Errors = new List<string>(errors);
        }

        public T Data { get; }
        public string Message { get; }
        public IList<string> Errors { get; }
        public bool IsSuccess => !Errors.Any();
        public Exception Exception { get; }
    }
}
