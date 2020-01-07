using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newbeststories.Models;
using Newbeststories.Services;

namespace Newbeststories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IStoryService _storyService;
        private const string _storyCacheKey = "STORY_CACHE_KEY";
        private const double _timeSpam = 60;
        private const int _take = 20;
        private const string IDS = "https://hacker-news.firebaseio.com/v0/beststories.json";
        private const string ITEM = "https://hacker-news.firebaseio.com/v0/item/{0}.json";

        public ValuesController(IMemoryCache memoryCache, IStoryService storyService)
        {
            _cache = memoryCache;
            _storyService = storyService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetAsync()
        {
            try
            {
                if (_cache.TryGetValue(_storyCacheKey, out IEnumerable<Story> stories))
                {
                    return Ok(stories);
                }
                List<Story> result = new List<Story>();
                IEnumerable<long> ids = await _storyService.getIds(IDS);
                foreach (long id in ids)
                {
                    result.Add(await _storyService.getStory(string.Format(ITEM, id)));
                }
                stories = result.OrderByDescending(s => s.score).Take(_take);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(_timeSpam));
                _cache.Set(_storyCacheKey, stories, cacheEntryOptions);
                return Ok(stories);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}