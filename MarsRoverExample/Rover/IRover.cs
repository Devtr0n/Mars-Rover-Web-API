using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsRoverExample.Rover
{
    public interface IRover
    {
        IPosition RoverPosition { get; set; }
        MarsRoverExample.Rover.Rover.Orientations RoverOrientation { get; set; }
        IPlateau RoverPlateau { get; set; }
        bool IsRobotInsideBoundaries { get; }
        void Process(string commands);
        string ToString();
    }
}