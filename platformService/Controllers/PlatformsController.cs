using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using platformService.AsyncDataServices;
using platformService.Data;
using platformService.Dtos;
using platformService.Models;

namespace platformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---> Getting platforms");
            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
             Console.WriteLine("---> Getting platforms by Id");
            var platformItems = _repository.GetPlatformById(id);
            if (platformItems != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItems));
            }
            return NotFound();
        }


        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
             Console.WriteLine("---> Creating platform");

            var platformModel = _mapper.Map<Platform>(platformCreateDto);
             _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
          
            if (platformModel != null)
            {
                var platform = _mapper.Map<PlatformReadDto>(platformModel);
                return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id}, platform );
            }
            return NotFound();
        }
    }

}