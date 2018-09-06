namespace MarsRoverExample.Models
{
    public class MarsRoverDto
    {
        public int RoverId { get; set; }
        public string RoverName { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public string CurrentDirection { get; set; }
    }
}