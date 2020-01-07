using System.Collections.Generic;
using System.Threading.Tasks;
using Newbeststories.Models;

namespace Newbeststories.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<long>> getIds(string URL);
        Task<Story> getStory(string URL);
    }
}