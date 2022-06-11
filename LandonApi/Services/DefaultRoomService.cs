using AutoMapper;
using AutoMapper.QueryableExtensions;
using LandonApi.Data;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelAPIDbContext _context;
        //private readonly IMapper _mapper;
        private IConfigurationProvider _mappingConfiguration;
        public DefaultRoomService(HotelAPIDbContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            //_mapper = mapper;
            _mappingConfiguration = mappingConfiguration;
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
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<Room>(entity);
          
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var query = _context.Rooms
                 .ProjectTo<Room>(_mappingConfiguration);

            return await query.ToArrayAsync();
        }
    }
}
