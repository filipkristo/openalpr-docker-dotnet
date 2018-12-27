using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenAlprDotNetService.Dto;
using OpenAlprDotNetService.Model;
using OpenAlprDotNetService.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenAlprDotNetService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicencePlateController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LicencePlateController> _logger;
        private readonly IMapper _mapper;
        private readonly IRecognitionService _recognitionService;

        public LicencePlateController(HttpClient httpClient, ILogger<LicencePlateController> logger, IMapper mapper, IRecognitionService recognitionService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
            _recognitionService = recognitionService;
        }

        [HttpPost]
        public IEnumerable<RecognitionResult> Post(RecognitionModelRequest model)
        {
            _logger.LogInformation("START => POST Recognition");

            var fileBytes = Convert.FromBase64String(model.Base64String);
            _logger.LogDebug("Converted base64 image to byte array");

            var result = _recognitionService.RecognizePlate(fileBytes);

            _logger.LogInformation("END => POST Recognition");
            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<RecognitionResult>> Get(string url)
        {
            _logger.LogInformation("START => GET Recognition");

            var fileBytes = await _httpClient.GetByteArrayAsync(url).ConfigureAwait(false);
            _logger.LogInformation($"Image downloaded from url: {url}");

            var result = _recognitionService.RecognizePlate(fileBytes);

            _logger.LogInformation("END => END Recognition");
            return result;
        }
    }
}
