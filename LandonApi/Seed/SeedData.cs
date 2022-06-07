using LandonApi.Data;
using LandonApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Seed
{
    /*
        What is the use of static class?
            Static classes are used as containers for static members.
            Static methods and static properties are the most-used members of a static class. 
            All static members are called directly using the class name. 
            Static methods do a specific job and are called directly using a type name, 
            rather than the instance of a type.
     */
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestData(services.GetRequiredService<HotelAPIDbContext>());
        }
        public static async Task AddTestData(HotelAPIDbContext context)
        {

            //check alreday has  any data and return
            if (context.Rooms.Any())
            {
                return;
            }
            // add seed data
            context.Rooms.Add(new RoomEntity
            {
                Id = Guid.Parse("301df04d-8679-4b1b-ab92-0a586ae53d08"),
                Name = "Oxford Suite",
                Rate = 10119,
            });

            context.Rooms.Add(new RoomEntity
            {
                Id = Guid.Parse("ee2b83be-91db-4de5-8122-35a9e9195976"),
                Name = "Driscoll Suite",
                Rate = 23959
            });

            await context.SaveChangesAsync();
        }
    }
}
