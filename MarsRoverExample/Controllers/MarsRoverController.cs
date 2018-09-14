using MarsRoverExample.Models;
using MarsRoverExample.Repositories;
using MarsRoverExample.Rover;
using MarsRoverExample.Services;
using System;
using System.Text.RegularExpressions;
using System.Web.Http;

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
        [Route("Create/{RoverId:int}/{RoverName}")]
        public IHttpActionResult Create(int RoverId, string RoverName)
        {
            // validate parameters, require 'RoverId' and 'RoverName'
            if (RoverId == 0 || string.IsNullOrEmpty(RoverName))
            {
                return BadRequest();
            }

            // create a new Mars Rover from user supplied parameters
            MarsRoverEntity marsRoverEntity = new MarsRoverEntity();
            marsRoverEntity.RoverId = RoverId;
            marsRoverEntity.RoverName = RoverName;
            marsRoverEntity.CurrentX = 0;
            marsRoverEntity.CurrentY = 0;
            marsRoverEntity.CurrentDirection = "N";

            // let's add the new Mars Rover
            _MarsRoverRepository.Add(marsRoverEntity);

            return Ok();
        }

        [HttpPut]
        [Route("Rename/{RoverId:int}/{RoverName}")]
        public IHttpActionResult Rename(int? RoverId, string RoverName)
        {
            // validate parameters, require 'RoverId' and 'RoverName'
            if (RoverId.Equals(null) || string.IsNullOrEmpty(RoverName))
            {
                return BadRequest();
            }

            // retrieve our Mars Rover by 'RoverId'
            var MarsRoverEntityToUpdate = _MarsRoverRepository.GetSingle(RoverId ?? 0);

            if (MarsRoverEntityToUpdate.Equals(null))
            {
                return NotFound();
            }

            // 'rename' the Mars Rover
            MarsRoverEntityToUpdate.RoverName = RoverName;

            // persist our changes to the Mars Rover
            var MarsRoverEntity = _MarsRoverRepository.Update(MarsRoverEntityToUpdate);

            return Ok();
        }

        [HttpPut]
        [Route("Move/{RoverId:int}/{MovementInstruction}")]
        public IHttpActionResult Move(int? RoverId, string MovementInstruction)
        {
            // validate parameters 'RoverId' & 'MovementInstruction', make sure not null or empty and make sure instructions does not allow spaces
            if (RoverId.Equals(null) || string.IsNullOrEmpty(MovementInstruction) || MovementInstruction.Contains(" "))
            {
                return BadRequest();
            }

            // validation - filter out the bad rover movement instructions
            var allowableLetters = "LRM";
            foreach (char c in MovementInstruction)
            {
                if (!allowableLetters.Contains(c.ToString()))
                    return BadRequest();
            }

            var MarsRoverEntityToUpdate = _MarsRoverRepository.GetSingle(RoverId ?? 0);

            if (MarsRoverEntityToUpdate.Equals(null))
            {
                return NotFound();
            }

            // 'move' the Mars Rover with our 'move instructions' 
            Position _position = new Position(MarsRoverEntityToUpdate.CurrentX, MarsRoverEntityToUpdate.CurrentY);
            Plateau _plateau = new Plateau(new Position(6, 6));
            Rover.Rover.Orientations orientation = (Rover.Rover.Orientations)Enum.Parse(typeof(Rover.Rover.Orientations), MarsRoverEntityToUpdate.CurrentDirection);
            var _rover = new Rover.Rover(_position, orientation, _plateau);
            _rover.Process(MovementInstruction); //perform the 'move'

            // update our Mars Rover values with the new coordinates
            MarsRoverEntityToUpdate.CurrentX = _rover.RoverPosition.X;
            MarsRoverEntityToUpdate.CurrentY = _rover.RoverPosition.Y;
            MarsRoverEntityToUpdate.CurrentDirection = _rover.RoverOrientation.GetStringValue();
            var MarsRoverEntity = _MarsRoverRepository.Update(MarsRoverEntityToUpdate);

            return Ok(_MarsRoverMapper.MapToDto(MarsRoverEntity));
        }

        [HttpGet]
        [Route("GetPosition/{RoverId:int?}")]
        public IHttpActionResult GetPosition(int? RoverId)
        {
            // validate parameters, require 'RoverId'
            if (RoverId.Equals(null))
            {
                return BadRequest();
            }

            // retrieve Mars Rover by 'RoverId'
            var MarsRoverEntity = _MarsRoverRepository.GetSingle(RoverId ?? 0);

            if (MarsRoverEntity.Equals(null))
            {
                return NotFound();
            }

            return Ok(_MarsRoverMapper.MapToDto(MarsRoverEntity));
        }

        #endregion

    }
}
