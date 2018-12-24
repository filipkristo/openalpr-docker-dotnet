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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenAlprDotNetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicencePlateController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LicencePlateController> _logger;
        private readonly IMapper _mapper;

        public LicencePlateController(HttpClient httpClient, ILogger<LicencePlateController> logger, IMapper mapper)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public IEnumerable<RecognitionResult> Post(RecognitionModelRequest model)
        {
            _logger.LogInformation("START => POST Recognition");

            var filePath = $"plate_{Guid.NewGuid()}.jpg";
            var fileBytes = Convert.FromBase64String(model.Base64String);
            _logger.LogDebug("Converted base64 to byte array");

            System.IO.File.WriteAllBytes(filePath, fileBytes);
            _logger.LogDebug($"bytes saved to {filePath}");

            _logger.LogInformation("Recognition start");
            var json = ShellHelper.Bash($"alpr --json -c eu {filePath}");
            _logger.LogInformation("Recognition ended");

            System.IO.File.Delete(filePath);
            _logger.LogDebug("Deleting temp file");

            _logger.LogDebug("Starting to convert object");
            var openAlprResult = JsonConvert.DeserializeObject<OpelAlprResult>(json);
            var result = _mapper.Map<IEnumerable<RecognitionResult>>(openAlprResult);

            _logger.LogInformation("END => POST Recognition");

            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<RecognitionResult>> Get(string url)
        {
            _logger.LogInformation("START => GET Recognition");

            var fileBytes = await _httpClient.GetByteArrayAsync(url).ConfigureAwait(false);
            var filePath = $"plate_{Guid.NewGuid()}.jpg";
            _logger.LogInformation($"Image downloaded from url: {url}");

            System.IO.File.WriteAllBytes(filePath, fileBytes);
            _logger.LogDebug($"bytes saved to {filePath}");

            _logger.LogInformation("Recognition start");
            var json = ShellHelper.Bash($"alpr --json -c eu {filePath}");
            _logger.LogInformation("Recognition ended");

            System.IO.File.Delete(filePath);
            _logger.LogDebug("Deleting temp file");

            _logger.LogDebug("Starting to convert object");
            var openAlprResult = JsonConvert.DeserializeObject<OpelAlprResult>(json);
            var result = _mapper.Map<IEnumerable<RecognitionResult>>(openAlprResult);

            _logger.LogInformation("END => END Recognition");

            return result;
        }
    }
}
