using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FactorialsApiTests
{
    public class IntegrationUnitTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public IntegrationUnitTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(3, 6)]
        public async Task GetResultByValueTestMethod(int value, long result)
        {
            var apiResponse = await _fixture.Client.GetAsync($"factorials/{value}");
            Assert.Equal(StatusCodes.Status200OK, (int)apiResponse.StatusCode);
            Assert.Equal($"{result}", await apiResponse.Content.ReadAsStringAsync());
        }

        [Theory]
        [InlineData(-3)]
        public async Task CheckBadRequest(int value)
        {
            var apiResponse = await _fixture.Client.GetAsync($"factorials/{value}");
            Assert.Equal(StatusCodes.Status400BadRequest, (int)apiResponse.StatusCode);
        }

        [Theory]
        [InlineData(2, "{0, 3}")]
        [InlineData(6, "{4, 7}")]
        [InlineData(19, "{7, null}")]
        public async Task GetNearestValueByValueTestMethod(int value, string result)
        {
            var apiResponse = await _fixture.Client.GetAsync($"factorials/{value}/nearest-value");
            Assert.Equal(StatusCodes.Status200OK, (int)apiResponse.StatusCode);
            Assert.Equal($"{result}", await apiResponse.Content.ReadAsStringAsync());
        }

        [Theory]
        [InlineData(400, "{4, 6}")]
        [InlineData(23546, "{7, 19}")]
        public async Task GetNearestValueByResultTestMethod(int value, string result)
        {
            var apiResponse = await _fixture.Client.GetAsync($"values/{value}/nearest-factorials");
            Assert.Equal(StatusCodes.Status200OK, (int)apiResponse.StatusCode);
            Assert.Equal($"{result}", await apiResponse.Content.ReadAsStringAsync());
        }
    }
}
