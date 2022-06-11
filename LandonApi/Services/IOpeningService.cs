using LandonApi.Models;
using System.Threading.Tasks;

namespace LandonApi.Services
{
    public interface IOpeningService
    {
        Task<PagedResults<Opening>> GetOpeningsAsync(PagingOptions pagingOptions);
    }
}
