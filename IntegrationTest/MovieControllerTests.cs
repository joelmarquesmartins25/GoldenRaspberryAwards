namespace MyApi.Tests.Integration
{
    using System.Text.Json;
    using GoldenRaspberryAwards;
    using GoldenRaspberryAwards.Models;
    using Microsoft.AspNetCore.Mvc.Testing;

    public class MovieControllerTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MovieControllerTests(WebApplicationFactory<Program> factory)
        {
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", "true");
            _factory = factory;
        }

        [Fact]
        public async Task GetMovies_ReturnsValidResponse()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");

            // Act
            HttpResponseMessage response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            List<Movie>? result = JsonSerializer.Deserialize<List<Movie>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(206, result.Count);
        }

        [Fact]
        public async Task GetAwardsIntervals_ReturnsValidResponse()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/movies/intervals");

            // Act
            HttpResponseMessage response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            AwardIntervalResponse? result = JsonSerializer.Deserialize<AwardIntervalResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Min);
            Assert.NotNull(result.Max);
            Assert.NotEmpty(result.Min);
            Assert.NotEmpty(result.Max);
            Assert.Single(result.Min);
            Assert.Single(result.Max);

            // Validar resultados
            AwardInterval min = result.Min.First();
            AwardInterval max = result.Max.First();
            Assert.Equal("Joel Silver", min.Producer);
            Assert.Equal("Matthew Vaughn", max.Producer);
            Assert.Equal(1, min.Interval);
            Assert.Equal(13, max.Interval);
        }
    }
}