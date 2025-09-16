using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using eCommerceApp.Application.DTOs.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using eCommerceApp.Host;

namespace eCommerceApp.Tests.E2E
{
    public class AuthTests : IClassFixture<WebApplicationFactory<Host.Program>>
    {
        private readonly HttpClient _client;

        public AuthTests(WebApplicationFactory<Host.Program> factory)
        {
            // folder root API
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseContentRoot(@"C:\Projects\eCommerceAppSolution\eCommerceApp.Host")
                .UseEnvironment("Testing");
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            });
        }

        [Fact]
        public async Task Login_Should_Return_Token_When_Credentials_Are_Valid()
        {
            // Arrange: sesuaikan dengan user yang ada di DB seed atau register dulu
            var loginRequest = new LoginUser
            {
                Email = "admin@admin.com",
                Password = "Admin@123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Authentication/login", loginRequest);

            // Assert
            response.EnsureSuccessStatusCode(); // status 200-299
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            result.Should().NotBeNull();
            result!.Token.Should().NotBeNullOrEmpty();
        }

        // Contoh model response login (sesuaikan dengan API)
        public class LoginResponse
        {
            public string Token { get; set; } = default!;
            public string? Message { get; set; }
        }
    }
}
