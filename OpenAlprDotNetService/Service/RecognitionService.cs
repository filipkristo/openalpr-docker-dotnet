using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenAlprDotNetService.Dto;
using OpenAlprDotNetService.Model;
using OpenAlprDotNetService.Service.Interface;

namespace OpenAlprDotNetService.Service
{
    public class RecognitionService : IRecognitionService
    {
        private readonly ILogger<RecognitionService> _logger;
        private readonly IMapper _mapper;

        public RecognitionService(ILogger<RecognitionService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IReadOnlyList<RecognitionResult> RecognizePlate(byte[] imageBytes)
        {
            var filePath = $"plate_{Guid.NewGuid()}.jpg";

            System.IO.File.WriteAllBytes(filePath, imageBytes);
            _logger.LogDebug($"image bytes saved to {filePath}");

            _logger.LogInformation("Recognition start");
            var json = ShellHelper.Bash($"alpr --config openalpr/openalpr.conf --json -c eu -p hr {filePath}");
            _logger.LogDebug($"Result: {json}");
            _logger.LogInformation("Recognition ended");

            System.IO.File.Delete(filePath);
            _logger.LogDebug("Deleting temp file");

            _logger.LogDebug("Starting to convert object");
            var openAlprResult = JsonConvert.DeserializeObject<OpelAlprResult>(json);
            var result = _mapper.Map<List<RecognitionResult>>(openAlprResult);

            return result;
        }
    }
}
