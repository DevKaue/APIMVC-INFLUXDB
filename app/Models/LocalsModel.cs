using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class LocalsModel
    {
        // public int CodeReference { get; init; }
        // public string DisplayText => $"O codigo da estuda e' {CodeReference} ft. at {Time}.";
        public string? Time { get; init; } = string.Empty;
        public float? Temperatura { get; set; }
        public float? Umidade { get; set; }
        public float? Gas { get; set; }
    }
}