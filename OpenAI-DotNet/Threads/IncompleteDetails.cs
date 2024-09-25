// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using OpenAI.Extensions;

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
