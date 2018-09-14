using MarsRoverExample.Models;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverExample.Repositories
{
    public class MarsRoverRepository : IMarsRoverRepository
    {
        readonly Dictionary<int, MarsRoverEntity> _MarsRovers = new Dictionary<int, MarsRoverEntity>();

        public MarsRoverRepository()
        {
            _MarsRovers.Add(1, new MarsRoverEntity() { RoverId = 1, RoverName = "Rover One", CurrentX = 0, CurrentY = 0, CurrentDirection = "N" }); // create the initial Mars Rover
        }

        public MarsRoverEntity GetSingle(int id)
        {
            return _MarsRovers.FirstOrDefault(x => x.Key == id).Value;
        }

        public MarsRoverEntity Add(MarsRoverEntity toAdd)
        {
            _MarsRovers.Add(toAdd.RoverId, toAdd);
            return toAdd;
        }

        public MarsRoverEntity Update(MarsRoverEntity toUpdate)
        {
            MarsRoverEntity single = GetSingle(toUpdate.RoverId);

            if (single == null)
            {
                return null;
            }

            _MarsRovers[single.RoverId] = toUpdate;
            return toUpdate;
        }

    }
}
