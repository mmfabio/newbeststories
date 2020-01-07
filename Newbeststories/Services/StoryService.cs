using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System;
using Newbeststories.Models;

namespace Newbeststories.Services
{
    public class StoryService: IStoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public StoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public DateTime getFormatedDate(long timestamp)
        {
            DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }

        public async Task<IEnumerable<long>> getIds(string URL)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL);
            HttpClient client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                IEnumerable<long> ids = await JsonSerializer.DeserializeAsync<IEnumerable<long>>(responseStream);
                return ids;
            }
            throw new Exception("Service 'getIds' unavailable");
        }

        public async Task<Story> getStory(string URL)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL);
            HttpClient client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                OriginalStory originalStory = await JsonSerializer.DeserializeAsync<OriginalStory>(responseStream);
                return new Story(
                    originalStory.id,
                    originalStory.title,
                    originalStory.url,
                    originalStory.by,
                    getFormatedDate(originalStory.time),
                    originalStory.score,
                    originalStory.descendants
                );
            }
            throw new Exception("Service 'getStory' unavailable");
        }
    }
}