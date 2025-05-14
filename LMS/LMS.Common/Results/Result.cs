using LMS.Common.Enums;

namespace LMS.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailed { get; }
        public ResponseStatus Status { get; }

        public static Result Success(ResponseStatus message) => new Result(true, message);
        public static Result Failure(ResponseStatus message) => new Result(false, message);

        protected Result(bool isSuccess, ResponseStatus message)
        {
            IsSuccess = isSuccess;
            IsFailed = !isSuccess;
            Status = message;
        }
    }

    public class Result<TEntity> : Result
    {
        public TEntity? Value { get; }

        public static Result<TEntity> Success(TEntity value, ResponseStatus message) => new Result<TEntity>(true, value, message);
        public static new Result<TEntity> Failure(ResponseStatus message) => new Result<TEntity>(false, default!, message);

        private Result(bool isSuccess, TEntity? value, ResponseStatus message) : base(isSuccess, message)
        {
            Value = value;
        }
    }
}
