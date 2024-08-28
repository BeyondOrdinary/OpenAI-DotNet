// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI
{
    public enum ChatResponseFormat
    {
        Auto = 0,
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "json_object")]
        Json,
        [EnumMember(Value = "json_schema")]
        JsonSchema
    }

    public class ChatResponseFormatConverter : JsonConverter<ChatResponseFormat>
    {
        public override ChatResponseFormat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string v = reader.GetString();
            switch(v)
            {
                case "text" : return ChatResponseFormat.Text;
                case "jason_object": return ChatResponseFormat.Json;
                case "json_schema": return ChatResponseFormat.JsonSchema;
                default:
                    return ChatResponseFormat.Auto;
            }
        }
        public override void Write(Utf8JsonWriter writer, ChatResponseFormat value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ChatResponseFormat.Text : writer.WriteStringValue("text"); break;
                case ChatResponseFormat.Json: writer.WriteStringValue("json_object"); break;
                case ChatResponseFormat.JsonSchema: writer.WriteStringValue("json_schema"); break;
                default: writer.WriteStringValue("text"); break;
            }
        }
    }

}
