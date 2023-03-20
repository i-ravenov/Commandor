using AutoMapper;
using Commandor.Data;
using Commandor.Dtos;
using Commandor.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commandor.Controllers
{

    //api/commands
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandorRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandorRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repo.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // Source -> Target
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repo.CreateCommand(commandModel);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repo.UpdateCommand(commandModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repo.UpdateCommand(commandModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeleteCommand(commandModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}