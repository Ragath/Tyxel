using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tyxel.Model
{
    public partial class ProjectConfigConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(ProjectConfig).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            for (var v = (int?)jObject["Version"] ?? 0; v < Migrations.Count; v = (int)jObject["Version"])
                Migrations[v](jObject);


            var result = existingValue as ProjectConfig ?? new ProjectConfig();
            serializer.Populate(jObject.CreateReader(), result);
            return result;
        }


        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}
