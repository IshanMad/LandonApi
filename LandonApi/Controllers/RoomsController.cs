using LandonApi.Data;
using LandonApi.Models;
using LandonApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //add api version
    [ApiVersion("1.0")]
    public class RoomsController:ControllerBase
    {
        private readonly IRoomService _roomService;
        // constructer
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet(Name =nameof(GetAllRooms))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<Room>>> GetAllRooms()
        {
            var rooms = await _roomService.GetRoomsAsync();

            var collection = new Collection<Room>
            {
                Self = Link.ToCollection(nameof(GetAllRooms)),
                Value = rooms.ToArray()
            };

            return collection;
        }


        [HttpGet("{roomId}",Name= nameof(GetRoomById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
        {
            var room = await _roomService.GetRoomAsync(roomId);
            if (room == null) return NotFound();
            return room;
        }
        public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings([FromQuery] PagingOptions pagingOptions = null)
        {

        }
    }
}
