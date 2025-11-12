namespace cubets_core.Common
{
    public interface IResponseService
    {
        ApiResponse<T> Success<T>(T data, string? message = null);
        ApiResponse<T> Fail<T>(string message);
    }
}
