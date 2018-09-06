using System.ComponentModel.DataAnnotations;

namespace MarsRoverExample.Models
{
    public class MarsRoverEntity
    {
        [Required]
        public int RoverId { get; set; }
        [Required]
        public string RoverName { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public string CurrentDirection { get; set; }
    }
}