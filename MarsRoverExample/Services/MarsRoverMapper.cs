using MarsRoverExample.Models;

namespace MarsRoverExample.Services
{
    public class MarsRoverMapper : IMarsRoverMapper
    {
        public MarsRoverDto MapToDto(MarsRoverEntity MarsRoverEntity)
        {
            return new MarsRoverDto()
            {
                RoverId = MarsRoverEntity.RoverId,
                RoverName = MarsRoverEntity.RoverName,
                CurrentX = MarsRoverEntity.CurrentX,
                CurrentY = MarsRoverEntity.CurrentY,
                CurrentDirection = MarsRoverEntity.CurrentDirection
            };
        }

        public MarsRoverEntity MapToEntity(MarsRoverDto MarsRoverDto)
        {
            return new MarsRoverEntity()
            {
                RoverId = MarsRoverDto.RoverId,
                RoverName = MarsRoverDto.RoverName,
                CurrentX = MarsRoverDto.CurrentX,
                CurrentY = MarsRoverDto.CurrentY,
                CurrentDirection = MarsRoverDto.CurrentDirection
            };
        }
    }
}