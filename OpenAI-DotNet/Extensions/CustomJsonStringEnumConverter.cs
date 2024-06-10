// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI.Extensions
{
    /// <summary>
    /// https://github.com/dotnet/runtime/issues/74385#issuecomment-1456725149
    /// </summary>
    internal sealed class CustomJsonStringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private readonly JsonNamingPolicy namingPolicy;
        private readonly Dictionary<int, TEnum> numberToEnum = new();
        private readonly Dictionary<TEnum, string> enumToString = new();
        private readonly Dictionary<string, TEnum> stringToEnum = new();

        public CustomJsonStringEnumConverter()
        {
            // We assume everything from OpenAI is snake case
            namingPolicy = new SnakeCaseNamingPolicy();
            var type = typeof(TEnum);

            FieldInfo[] fi = typeof(TEnum).GetFields();
            foreach (FieldInfo field in fi)
            {
                if (field.Name.Equals("value__"))
                {
                    continue; // skip this one
                }
                var attribute = field.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                    .Cast<EnumMemberAttribute>()
                    .FirstOrDefault();
                TEnum tv = (TEnum)Enum.Parse(typeof(TEnum), field.Name);
                numberToEnum.Add((int)field.GetRawConstantValue(), tv);
                // The name values might need adjustment for camel case, or other
                // variants. Need to map them here
                if (attribute?.Value != null)
                {
                    enumToString.Add(tv, attribute.Value);
                    stringToEnum.Add(attribute.Value, tv);
                }
                else
                {
                    var convertedName = namingPolicy != null
                        ? namingPolicy.ConvertName(field.Name)
                        : field.Name;
                    enumToString.Add(tv, convertedName);
                    stringToEnum.Add(convertedName, tv);
                }
            }
            /*
            foreach (int value in Enum.GetValues(typeof(TEnum)))
            {
                var enumMember = type.GetMember(value.ToString())[0];
                var attribute = enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                    .Cast<EnumMemberAttribute>()
                    .FirstOrDefault();
                var index = Convert.ToInt32(type.GetField("value__")?.GetValue(value));

                TEnum vv = (TEnum)Enum.Parse(typeof(TEnum), index.ToString());
                if (attribute?.Value != null)
                {
                    numberToEnum.Add(index, vv);
                    enumToString.Add(vv, attribute.Value);
                    stringToEnum.Add(attribute.Value, vv);
                }
                else
                {
                    var convertedName = namingPolicy != null
                        ? namingPolicy.ConvertName(value.ToString())
                        : value.ToString();
                    numberToEnum.Add(index, vv);
                    enumToString.Add(vv, convertedName);
                    stringToEnum.Add(convertedName, vv);
                }
            }
            */
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var type = reader.TokenType;

            switch (type)
            {
                case JsonTokenType.String:
                    {
                        var stringValue = reader.GetString();

                        if (stringValue != null)
                        {
                            var value = namingPolicy != null
                                ? namingPolicy.ConvertName(stringValue)
                                : stringValue;

                            if (stringToEnum.TryGetValue(value, out var enumValue))
                            {
                                return enumValue;
                            }
                        }

                        break;
                    }
                case JsonTokenType.Number:
                    {
                        var numValue = reader.GetInt32();
                        numberToEnum.TryGetValue(numValue, out var enumValue);
                        return enumValue;
                    }
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
            => writer.WriteStringValue(enumToString[value]);
    }
}
