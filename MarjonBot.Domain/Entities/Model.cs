using Newtonsoft.Json;

namespace MarjonBot.Domain.Entities;

public class Model
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("mark")]
    public int Mark { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
}
