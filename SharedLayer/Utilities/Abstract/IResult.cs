namespace SharedLayer.Utilities.Abstract
{
    public interface IResult<out T>
    {
        public T Data { get; }
        public string Message { get; }
        public IList<string> Errors { get; }
        public bool IsSuccess { get; }
        public Exception Exception { get; }
    }
}