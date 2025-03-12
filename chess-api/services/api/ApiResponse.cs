namespace Chess
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public ApiResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public ApiResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = "";
        }
    }
}
