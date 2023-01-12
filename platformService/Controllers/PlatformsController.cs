using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using platformService.Data;
using platformService.Dtos;

namespace platformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

    }

}