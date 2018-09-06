using System.Web.Http;
using MarsRoverExample.Models;
using MarsRoverExample.Repositories;
using MarsRoverExample.Services;
using System.Text.RegularExpressions;

namespace MarsRoverExample.Controllers
{
    [RoutePrefix("api/MarsRover")]
    public class MarsRoverController : ApiController
    {

        #region API Constructor

        private readonly IMarsRoverRepository _MarsRoverRepository;
        private readonly IMarsRoverMapper _MarsRoverMapper;

        public MarsRoverController(IMarsRoverRepository MarsRoverRepository, IMarsRoverMapper MarsRoverMapper)
        {
            _MarsRoverRepository = MarsRoverRepository;
            _MarsRoverMapper = MarsRoverMapper;
        }

        #endregion 

        #region Action Result methods

        [HttpPost]
        [Route("Create/{id:int}/{name}")]
        public IHttpActionResult Create(int id, string name)
        {
            // validate parameters, require 'id' and 'name'
            if (id == 0 || string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            // create a new mars rover from user supplied parameters
            MarsRoverEntity marsRoverEntity = new MarsRoverEntity();
            marsRoverEntity.RoverId = id;
            marsRoverEntity.RoverName = name;
            marsRoverEntity.CurrentX = 0;
            marsRoverEntity.CurrentY = 0;
            marsRoverEntity.CurrentDirection = "N";

            // let's add it
            _MarsRoverRepository.Add(marsRoverEntity);

            return Ok();
        }

        [HttpPut]
        [Route("Rename/{id:int}/{newName}")]
        public IHttpActionResult Rename(int? id, string newName)
        {
            // validate parameters, require 'id' and 'newName'
            if (id.Equals(null) || string.IsNullOrEmpty(newName))
            {
                return BadRequest();
            }

            // retrieve our Mars Rover by 'id'
            var MarsRoverEntityToUpdate = _MarsRoverRepository.GetSingle(id ?? 0);

            if (MarsRoverEntityToUpdate.Equals(null))
            {
                return NotFound();
            }

            // 'rename' the Mars Rover
            MarsRoverEntityToUpdate.RoverName = newName;

            // persist our changes to the Mars Rover
            var MarsRoverEntity = _MarsRoverRepository.Update(MarsRoverEntityToUpdate);

            return Ok();
        }

        [HttpPut]
        [Route("Move/{id:int}/{instruction}")]
        public IHttpActionResult Move(int? id, string instruction)
        {
            // validate parameters 'id' & 'instruction', make sure not null or empty
            if (id.Equals(null) || string.IsNullOrEmpty(instruction))
            {
                return BadRequest();
            }

            // validation - filter out the bad rover instructions via regular expression 
            var regex = new Regex(@"L|R|M"); // only allow characters "L", "R", and "M" (per requirements)
            var match = regex.Match(instruction);
            if (!match.Success)
            {
                return BadRequest(); //to-do: fix me with custom response? not enough time to get fancy....
            }

            var MarsRoverEntityToUpdate = _MarsRoverRepository.GetSingle(id ?? 0);

            if (MarsRoverEntityToUpdate.Equals(null))
            {
                return NotFound();
            }

            // to-do: 'move' the Mars Rover here, perform calculations and changes/updates here ??...
            foreach(char c in instruction)
            {
                switch (c)
                {
                    case 'L':
                        //MarsRoverEntityToUpdate.CurrentX = ???
                        break;
                    case 'R':
                        //MarsRoverEntityToUpdate.CurrentY = ???
                        break;
                    case 'M':
                        //MarsRoverEntityToUpdate.CurrentDirection = ??? based off cardinal compass points
                        break;
                }
            }

            // update our Mars Rover's new 'move'
            var MarsRoverEntity = _MarsRoverRepository.Update(MarsRoverEntityToUpdate); 

            return Ok(_MarsRoverMapper.MapToDto(MarsRoverEntity));
        }

        [HttpGet]
        [Route("GetPosition/{id:int?}")]
        public IHttpActionResult GetPosition(int? id)
        {
            // validate parameters, require 'id'
            if (id.Equals(null))
            {
                return BadRequest();
            }

            // retrieve Mars Rover by id
            var MarsRoverEntity = _MarsRoverRepository.GetSingle(id ?? 0);

            if (MarsRoverEntity.Equals(null))
            {
                return NotFound();
            }

            return Ok(_MarsRoverMapper.MapToDto(MarsRoverEntity));
        }

        #endregion

    }
}