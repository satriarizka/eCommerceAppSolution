namespace eCommerceApp.Application.DTOs
{
    public record LoginResponse(
    bool Success = false,
    string? Message = null,
    string? Token = null,
    string? RefreshToken = null
    )
    {
        public LoginResponse(string token, string refreshToken)
            : this(true, string.Empty, token, refreshToken) { }
    };
}
