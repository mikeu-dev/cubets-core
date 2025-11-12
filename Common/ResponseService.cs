namespace cubets_core.Common
{
    public class ResponseService: IResponseService
    {
        public ApiResponse<T> Success<T>(T data, string? message = null)
        {
            return new ApiResponse<T>(true, data, message);
        }

        public ApiResponse<T> Fail<T>(string message)
        {
            return new ApiResponse<T>(false, default, message);
        }
    }
}
