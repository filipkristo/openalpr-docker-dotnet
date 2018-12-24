using System;
using System.Collections.Generic;

namespace OpenAlprDotNetService.Dto
{
    public class RecognitionResult
    {
        public string PlateNumber { get; set; }
        public double Confidence { get; set; }
        public double TotalProcessingTimeMs { get; set; }
        public IEnumerable<PlatePoint> PlatePoints { get; set; }
    }
}
