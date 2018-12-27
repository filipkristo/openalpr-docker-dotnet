using System;
using System.Collections.Generic;
using OpenAlprDotNetService.Dto;

namespace OpenAlprDotNetService.Service.Interface
{
    public interface IRecognitionService
    {
        IReadOnlyList<RecognitionResult> RecognizePlate(byte[] imageBytes);
    }
}
