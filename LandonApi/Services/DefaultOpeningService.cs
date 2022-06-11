using AutoMapper;
using LandonApi.Data;

namespace LandonApi.Services
{
    public class DefaultOpeningService:IOpeningService
    {
        private readonly HotelAPIDbContext _context;
        private readonly IDateLogicService dateLogicService;
        private readonly IMapper _mapper;
    }
}
