namespace eCommerceApp.Domain.Entities.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiredAt { get; set; }
    }
}
