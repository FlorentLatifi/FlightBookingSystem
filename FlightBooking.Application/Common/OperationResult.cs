using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public string? Error { get; private set; }
        public Exception? Exception { get; private set; }

        private OperationResult() { }

        public static OperationResult<T> Success(T value)
            => new OperationResult<T> { IsSuccess = true, Value = value };

        public static OperationResult<T> Failure(string error, Exception? exception = null)
            => new OperationResult<T> { IsSuccess = false, Error = error, Exception = exception };

        public override string? ToString()
            => IsSuccess ? $"Success: {Value}" : $"Failure: {Error}";
    }

    // Convenience non-generic version (opsionale)
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }
        public Exception? Exception { get; private set; }

        private OperationResult() { }

        public static OperationResult Success()
            => new OperationResult { IsSuccess = true };

        public static OperationResult Failure(string error, Exception? exception = null)
            => new OperationResult { IsSuccess = false, Error = error, Exception = exception };
    }
}

