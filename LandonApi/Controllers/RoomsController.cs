using Microsoft.AspNetCore.Mvc;
using System;

namespace LandonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController:ControllerBase
    {
        [HttpGet(Name =nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        }
    }
}
