using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsRoverExample.Rover
{
    public class Plateau : IPlateau
    {
        public Position PlateauPosition { get; private set; }

        public Plateau(Position position)
        {
            PlateauPosition = position;
        }
    }
}