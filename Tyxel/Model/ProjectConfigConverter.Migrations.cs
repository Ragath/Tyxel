using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tyxel.Model
{
    public partial class ProjectConfigConverter
    {
        internal static IReadOnlyList<Action<JObject>> Migrations { get; } = new Action<JObject>[]
        {
            data => data["Version"] = 1
        };
    }
}
