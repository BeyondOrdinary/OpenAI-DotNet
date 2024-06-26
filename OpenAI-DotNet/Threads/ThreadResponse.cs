// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenAI.Threads
{
    /// <summary>
    /// A conversation session between an Assistant and a user.
    /// Threads store Messages and automatically handle truncation to fit content into a model's context.
    /// </summary>
    public sealed class ThreadResponse : BaseResponse, IServerSentEvent
    {
        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("id")]
        public string Id { get; private set; }

        /// <summary>
        /// The object type, which is always thread.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("object")]
        public string Object { get; private set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the thread was created.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTimeSeconds { get; private set; }

        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime;

        /// <summary>
        /// A set of resources that are made available to the assistant's tools in this thread.
        /// The resources are specific to the type of tool.
        /// For example, the code_interpreter tool requires a list of file IDs,
        /// while the file_search tool requires a list of vector store IDs.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("tool_resources")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ToolResources ToolResources { get; private set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IReadOnlyDictionary<string, string> Metadata { get; private set; }

        public static implicit operator string(ThreadResponse thread) => thread?.ToString();

        public override string ToString() => Id;
    }
}
