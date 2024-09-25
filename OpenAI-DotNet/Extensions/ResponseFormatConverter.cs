// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI
{
    // New Version
    public sealed class xxResponseFormatObject
    {
        public xxResponseFormatObject() { }

        public xxResponseFormatObject(ChatResponseFormat type)
        {
            if (type == ChatResponseFormat.JsonSchema)
            {
                throw new System.ArgumentException("Use the constructor overload that accepts a JsonSchema object for ChatResponseFormat.JsonSchema.", nameof(type));
            }
            Type = type;
        }

        [JsonInclude]
        [JsonPropertyName("type")]
        [JsonConverter(typeof(Extensions.JsonStringEnumConverter<ChatResponseFormat>))]
        public ChatResponseFormat Type { get; private set; }

        [JsonInclude]
        [JsonPropertyName("json_schema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public JsonSchema JsonSchema { get; private set; }

        public static implicit operator xxResponseFormatObject(ChatResponseFormat type) => new(type);

        public static implicit operator ChatResponseFormat(xxResponseFormatObject format) => format.Type;
    }
}

namespace OpenAI.Extensions
{

    internal sealed class ResponseFormatConverter : JsonConverter<ResponseFormatObject>
    {
        public override ResponseFormatObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType is JsonTokenType.Null or JsonTokenType.String)
                {
                    return ChatResponseFormat.Auto;
                }

                return JsonSerializer.Deserialize<ResponseFormatObject>(ref reader, options);
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading {typeof(ChatResponseFormat)} from JSON.", e);
            }
        }

        public override void Write(Utf8JsonWriter writer, ResponseFormatObject value, JsonSerializerOptions options)
        {
            // serialize the object normally
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
