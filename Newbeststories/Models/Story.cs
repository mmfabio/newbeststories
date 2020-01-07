using System;

namespace Newbeststories.Models
{
    public class Story
    {
        public long id { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public string postedBy { get; set; }
        public DateTime time { get; set; }
        public int score { get; set; }
        public int commentCount { get; set; }

        public Story(long id, string title, string uri, string postedBy, DateTime time, int score, int commentCount)
        {
            if(id < 1)
                throw new ArgumentException("Invalid id");
            if(string.IsNullOrEmpty(title))
                throw new ArgumentException($"Invalid title - ID: {id}");
            if(string.IsNullOrEmpty(postedBy))
                throw new ArgumentException($"Invalid posted by - ID: {id}");

            this.id = id;
            this.title = title;
            this.uri = uri;
            this.postedBy = postedBy;
            this.time = time;
            this.score = score;
            this.commentCount = commentCount;
        }
    }
}