using System;
using Newtonsoft.Json;

namespace OpenAlprDotNetService.Model
{
    public class PlateCandidates
    {
        public string Plate { get; set; }

        public double Confidence { get; set; }

        [JsonProperty("matches_template")]
        public byte MatchesTemplate { get; set; }
    }
}
