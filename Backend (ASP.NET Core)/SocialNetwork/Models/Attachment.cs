﻿using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; } // "image", "video", "url" ...

        public int PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }

    }
}
