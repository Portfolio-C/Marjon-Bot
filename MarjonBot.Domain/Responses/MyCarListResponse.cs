using MarjonBot.Domain.Entities;
using Newtonsoft.Json;

namespace MarjonBot.Domain.Responses;

public class MyCarListResponse
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("results")]
    public List<Car> Cars { get; set; }
}
