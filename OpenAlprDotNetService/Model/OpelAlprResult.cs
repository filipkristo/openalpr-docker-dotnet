using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenAlprDotNetService.Model
{
    public class OpelAlprResult
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("img_width")]
        public int ImageWidth { get; set; }

        [JsonProperty("img_height")]
        public int ImageHeight { get; set; }

        [JsonProperty("processing_time_ms")]
        public double ProcessingTimeMs { get; set; }

        [JsonProperty("results")]
        public IEnumerable<PlateResult> Results { get; set; }

    }
}
