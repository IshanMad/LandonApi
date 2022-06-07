using AutoMapper;
using LandonApi.Data;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelAPIDbContext _context;
        private readonly IMapper _mapper;
        public DefaultRoomService(HotelAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }    
        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return null;
            }
            /*return new Room
            {
                Href = null,//Url.Link(nameof(GetRoomById), new { roomId = entity.Id }),
                Name = entity.Name,
                Rate = entity.Rate / 100.0m
            };*/
            return _mapper.Map<Room>(entity);
          
        }
    }
}
