using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsRoverExample.Rover
{
    public interface IPosition
    {
        int X { get; set; }
        int Y { get; set; }
        string ToString();
    }
}