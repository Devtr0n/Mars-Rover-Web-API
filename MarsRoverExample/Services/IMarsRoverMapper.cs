using MarsRoverExample.Models;

namespace MarsRoverExample.Services
{
    public interface IMarsRoverMapper
    {
        MarsRoverDto MapToDto(MarsRoverEntity MarsRoverEntity);
        MarsRoverEntity MapToEntity(MarsRoverDto MarsRoverDto);
    }
}