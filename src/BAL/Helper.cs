using ConsoleAppNet5.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet5.BAL
{
    public static class Helper
    {
        /// <summary>
        /// Set rover left and right direction
        /// </summary>
        /// <param name="rover">rover options</param>
        /// <returns>rover direction</returns>
        /// <exception cref="ArgumentException"></exception>
        public static RoverOptions SetDirection(RoverOptions rover)
        {
            rover.LSide = rover.Direction switch
            {
                Constant.E => Constant.N,
                Constant.N => Constant.W,
                Constant.W => Constant.S,
                Constant.S => Constant.E,
                _ => throw new ArgumentException(message: "invlaid direction"),
            };

            rover.RSide = rover.Direction switch
            {
                Constant.E => Constant.S,
                Constant.S => Constant.W,
                Constant.W => Constant.N,
                Constant.N => Constant.E,
                _ => throw new ArgumentException(message: "invlaid direction"),
            };

            return rover;
        }

        /// <summary>
        /// move rover direction
        /// </summary>
        /// <param name="instruction">instruction</param>
        /// <param name="rover">rover</param>
        /// <returns>rover</returns>
        /// <exception cref="ArgumentException"></exception>
        public static RoverOptions MoveDirection(char instruction, RoverOptions rover)
        {
            rover.Direction = instruction switch
            {
                Constant.L => rover.LSide,
                Constant.R => rover.RSide,
                _ => throw new ArgumentException(message: "invlaid instruction"),
            };
            SetDirection(rover);
            return rover;
        }

        /// <summary>
        /// Move rover position.
        /// </summary>
        /// <param name="rover">rover</param>
        /// <returns>position</returns>
        public static RoverOptions MovePosition(RoverOptions rover)
        {
            //TODO: Need to check co-orinate value and set limit if reach threashold.

            switch (rover.Direction)
            {
                case Constant.N:
                    rover.Y += 1;
                    if (rover.Y == 0) rover.Y = 1;
                    break;

                case Constant.E:
                    rover.X += 1;
                    if (rover.X == 0) rover.X = 1;
                    break;

                case Constant.W:
                    rover.X -= 1;
                    if (rover.X == 0) rover.X = -1;
                    break;

                case Constant.S:
                    rover.Y -= 1;
                    if (rover.X == 0) rover.X = -1;
                    break;
            }

            return rover;
        }
    }
}
