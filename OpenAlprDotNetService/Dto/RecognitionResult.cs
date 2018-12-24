using System;
using System.Collections.Generic;

namespace OpenAlprDotNetService.Dto
{
    public class RecognitionResult
    {
        public string PlateNumber { get; set; }
        public float Confidence { get; set; }
        public float TotalProcessingTimeMs { get; set; }
        public IEnumerable<PlatePoint> PlatePoints { get; set; }
    }
}
