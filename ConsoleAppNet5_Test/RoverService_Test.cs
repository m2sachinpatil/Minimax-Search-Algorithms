using ConsoleAppNet5.Configuration;
using ConsoleAppNet5.Services;
using Microsoft.Extensions.Logging;

namespace ConsoleAppNet5_Test
{
    [TestFixture]
    public class RoverService_Test
    {
        private RoverService _roverService;
        private readonly ILogger<RoverService>? _logger;

        [SetUp]
        public void Setup()
        {
             _roverService = new RoverService(_logger);
        }

        //Success cases
        [TestCase(1, 2, 'N', "LMLMLMLMM", ExpectedResult = "Output: 1 3 N")]
        [TestCase(3, 3, 'E', "MMRMMRMRRM", ExpectedResult = "Output: 5 1 E")]
        public async Task<string> DoWork_Sucess_Result(int x, int y, char d, string movmentPlan)
        {
            // Arrange
            var roverData = new RoverOptions
            {
                X = x,
                Y = y,
                Direction = d
            };

            //Act
            var result = await _roverService.DoWork(roverData, movmentPlan.ToList<char>());

            //Assert
            return result;
        }

        //Un Sucess cases
        [TestCase(1, 2, 'N', "", ExpectedResult = null)]
        public async Task<string> DoWork_Un_Sucess_Result(int x, int y, char d, string movmentPlan)
        {
            //Arrange
            var roverData = new RoverOptions
            {
                X = x,
                Y = y,
                Direction = d
            };

            //Act
            var result = await _roverService.DoWork(roverData, movmentPlan.ToList<char>());

            //Assert
            return result;
        }


        //Exception Cases
        [Test]
        public void DoWork_Exception_Result()
        {
            //Arrange
            var roverData = new RoverOptions
            {
                X = 1,
                Y = 2,
                Direction = 'T'
            };

            //Act
            var movmentPlan = "MMRMMRMRRM".ToList<char>();

            //Assert
            Assert.CatchAsync<ArgumentException>(async () => await _roverService.DoWork(roverData, movmentPlan));
        }
    }
}
