using System.Net;

namespace Application.Common.Responses;

public class Result<TResponse>
{
    public bool IsSuccess { get; }
    public TResponse? Data { get; }
    public string? ErrorMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }

    public Result(TResponse data, bool isSuccess, string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        Data = data;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        HttpStatusCode = httpStatusCode;
    }

    public Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public Result(TResponse data, bool isSuccess)
    {
        Data = data;
        IsSuccess = isSuccess;
    }

    public static Result<TResponse> Success(TResponse data) => new(data, true, string.Empty);
    public static Result<TResponse> Failure(string errorMessage) => new(default!, false, errorMessage);
    public static Result<TResponse> SuccessWithStatusCode(HttpStatusCode httpStatusCode)
        => new(default!, true, string.Empty, httpStatusCode);
    public static Result<TResponse> SuccessWithStatusCode(TResponse data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        => new(data, true, string.Empty, httpStatusCode);
    public static Result<TResponse> FailureWithStatusCode(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) =>
       new(default!, false, errorMessage, httpStatusCode);
}
