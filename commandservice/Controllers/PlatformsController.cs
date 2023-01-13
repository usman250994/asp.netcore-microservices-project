using System;
using Microsoft.AspNetCore.Mvc;
namespace commandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("---> InboundPost # Command Service");
            return Ok("Inbound test from Platform Controller");
        }
    }
}