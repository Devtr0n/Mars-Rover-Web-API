using MarsRoverExample.Models;

namespace MarsRoverExample.Repositories
{
    public interface IMarsRoverRepository
    {
        MarsRoverEntity GetSingle(int id);
        MarsRoverEntity Add(MarsRoverEntity toAdd);
        MarsRoverEntity Update(MarsRoverEntity toUpdate);
    }
}
