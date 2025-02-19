﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LandonApi.Data;
using LandonApi.Models;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelAPIDbContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultRoomService(
            HotelAPIDbContext context,
            IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<Room>(entity);
        }

        public async Task<PagedResults<Room>> GetRoomsAsync(
            PagingOptions pagingOptions,
            SortOptions<Room, RoomEntity> sortOptions)
        {
            IQueryable<RoomEntity> query = _context.Rooms;
            query = sortOptions.Apply(query);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<Room>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<Room>
            {
                Items = items,
                TotalSize = size
            };
        }
    }
}
