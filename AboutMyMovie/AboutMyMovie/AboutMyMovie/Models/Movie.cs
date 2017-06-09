using Newtonsoft.Json;

namespace AboutMyMovie.Models
{
    public class Movie
    {
        [JsonProperty(PropertyName = "Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Year", NullValueHandling = NullValueHandling.Ignore)]
        public string Year { get; set; }
        [JsonProperty(PropertyName = "Released", NullValueHandling = NullValueHandling.Ignore)]
        public string Released { get; set; }
        [JsonProperty(PropertyName = "Genre", NullValueHandling = NullValueHandling.Ignore)]
        public string Genre { get; set; }
        [JsonProperty(PropertyName = "Director", NullValueHandling = NullValueHandling.Ignore)]
        public string Director { get; set; }
    }
}
