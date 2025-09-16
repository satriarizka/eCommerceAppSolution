namespace eCommerceApp.Application.DTOs
{
    public record ServiceResponse(bool Success = false, string? Message = null)
    {
        public static implicit operator ServiceResponse(string message)
            => new(false, message);

        public static implicit operator ServiceResponse(bool success)
            => new(success, success ? "Success" : "Failed");

        public static implicit operator ServiceResponse((bool Success, string Message) tuple)
            => new(tuple.Success, tuple.Message);
    }
}
