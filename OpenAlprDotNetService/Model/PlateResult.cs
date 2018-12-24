using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAlprDotNetService.Model
{
    public class PlateResult
    {
        public string Plate { get; set; }

        public double Confidence { get; set; }

        [JsonProperty("processing_time_ms")]
        public double ProcessingTimeMs { get; set; }

        public IEnumerable<PlateCoordinate> Coordinates { get; set; }
    }
}
