﻿using LandonApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public interface IRoomService
    {
        Task<PagedResults<Room>> GetRoomsAsync(
           PagingOptions pagingOptions,
           SortOptions<Room, RoomEntity> sortOptions);


        Task<Room> GetRoomAsync(Guid id);
    }
}
