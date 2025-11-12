using Microsoft.AspNetCore.Http.HttpResults;

namespace cubets_core.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public long ServerTime { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool success, T? data = default, string? message = null)
        {
            Success = success;
            Message = message;
            ServerTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Data = data;
        }
    }
}
