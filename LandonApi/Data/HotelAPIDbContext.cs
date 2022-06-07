using LandonApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LandonApi.Data
{
    public class HotelAPIDbContext:DbContext
    {
        public HotelAPIDbContext(DbContextOptions<HotelAPIDbContext> options) : base(options)
        {

        }
        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
/*
 *What is DbContext used for?
     A DbContext instance represents a combination of the Unit Of Work and Repository
     patterns such that it can be used to query from a database and group together changes
     that will then be written back to the store as a unit. DbContext is conceptually similar to ObjectContext.
 *The DbContext allows you to link your model properties (presumably using the Entity Framework)
  to your database with a connection string.
 *Entity Framework - DbContext
    1.Materialize data returned from the database as entity objects.
    2.Track changes that were made to the objects.
    3.Handle concurrency.
    4.Propagate object changes back to the database.
    5.Bind objects to controls
 */