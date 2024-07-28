namespace WebAPI_Blackout.Models
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }
        public static ApiResponse<T> ErrorResponse(string error) 
        {
            return new ApiResponse<T>
            {
                Success = false,
                Error = error,
                Data = null
            };
        }
        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Error = null,
                Data = data
            };
        }
    }
}
