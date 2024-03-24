namespace UserManager.Contracts.Responses
{
    public class ResponseApi
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public ResponseApi() { }

        public ResponseApi(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public ResponseApi(bool success, string message, int id)
        {
            Success = success;
            Message = message;
            Id = id;
        }
    }
}
