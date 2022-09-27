using ConsoleAppNet5.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using ConsoleAppNet5.BAL;
using System.Collections.Generic;

namespace ConsoleAppNet5.Services
{
    public interface IRoverService
    {
        Task<string> DoWork(RoverOptions roverPosition, List<char> momentPlan);
    }

    public class RoverService : IRoverService
    {
        private readonly ILogger<RoverService> _logger;

        public RoverService(
             ILogger<RoverService> logger
        )
        {
            _logger = logger;
        }

        /// <summary>
        /// Operation method
        /// </summary>
        /// <returns>boolean to host</returns>
        public async Task<string> DoWork(RoverOptions roverData, List<char> movmentPlan)
        {
            try
            {        
                    Helper.SetDirection(roverData);

                    if (movmentPlan.Count != 0)
                    {
                        foreach (char instruction in movmentPlan)
                        {
                            switch (instruction)
                            {
                                case Constant.L:
                                case Constant.R:
                                    Helper.MoveDirection(instruction, roverData);
                                    break;

                                case Constant.M:
                                    Helper.MovePosition(roverData);
                                    break;

                                default:
                                    throw new InvalidDataException("invalid instruction");
                            }
                        }

                        return($"Output: {roverData.X} {roverData.Y} {roverData.Direction}");
                    }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(DoWork)} failed", ex.Message);
            }

            return null;
        }
    }
}
