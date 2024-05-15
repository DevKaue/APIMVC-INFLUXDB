using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class AltitudeModel
    {
        public string Time { get; init; } = string.Empty;
        public int Altitude { get; init; }
        public string DisplayText => $"Plane was at altitude {Altitude} ft. at {Time}.";
    }
}