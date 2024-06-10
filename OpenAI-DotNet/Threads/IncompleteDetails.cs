using OpenAI.Extensions;
using System.Text.Json.Serialization;

namespace OpenAI.Threads
{
    public sealed class IncompleteDetails
    {
        [JsonInclude]
        [JsonPropertyName("reason")]
        [JsonConverter(typeof(CustomJsonStringEnumConverter<IncompleteMessageReason>))]
        public IncompleteMessageReason Reason { get; private set; }
    }
}
