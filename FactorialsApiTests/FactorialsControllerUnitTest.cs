using FactorialsApi;
using FactorialsApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FactorialsApiTests
{
    public class FactorialsControllerUnitTest
    {
        private readonly Mock<IRepository> _repository;
        private readonly FactorialsController _controller;

        public FactorialsControllerUnitTest()
        {
            _repository = new Mock<IRepository>();
            _controller = new FactorialsController(_repository.Object);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(0)]
        public async Task GetResultByValueTestMethod(int? test)
        {
            var result = await _controller.GetResultByValue(test);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetNearestValueByValueTestMethod()
        {
            var test = 10;
            var result = await _controller.GetNearestValueByValue(test);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetNearestValueByResultTestMethod()
        {
            var test = 10;
            var result = await _controller.GetNearestValueByResult(test);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
