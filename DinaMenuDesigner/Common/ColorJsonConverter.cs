using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace DinaMenuDesigner.Common
{
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            byte a = 255, r = 0, g = 0, b = 0;
            reader.Read(); // StartObject
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName)
                    {
                        case "A":
                            a = reader.GetByte();
                            break;
                        case "R":
                            r = reader.GetByte();
                            break;
                        case "G":
                            g = reader.GetByte();
                            break;
                        case "B":
                            b = reader.GetByte();
                            break;
                    }
                }
                reader.Read();
            }
            return Color.FromArgb(a, r, g, b);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("A", value.A);
            writer.WriteNumber("R", value.R);
            writer.WriteNumber("G", value.G);
            writer.WriteNumber("B", value.B);
            writer.WriteEndObject();
        }
    }
}
