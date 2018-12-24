using System;
using System.Collections.Generic;
using OpenAlprDotNetService.Model;

namespace OpenAlprDotNetService.Dto
{
    public class RecognitionResult
    {
        public string PlateNumber { get; set; }

        public double Confidence { get; set; }

        public double TotalProcessingTimeMs { get; set; }

        public IEnumerable<PlatePoint> PlatePoints { get; set; }

        public IEnumerable<PlateCandidates> Candidates { get; set; }
    }
}
