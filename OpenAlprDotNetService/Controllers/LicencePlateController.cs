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
            var filePath = $"plate_{Guid.NewGuid()}.jpg";
            var fileBytes = Convert.FromBase64String(model.Base64String);

            System.IO.File.WriteAllBytes(filePath, fileBytes);
            var json = ShellHelper.Bash($"alpr --json -c eu {filePath}");
            System.IO.File.Delete(filePath);

            var result = JsonConvert.DeserializeObject<OpelAlprResult>(json);
            return _mapper.Map<IEnumerable<RecognitionResult>>(result);
        }

        [HttpGet]
        public async Task<string> Get(string url)
        {
            var fileBytes = await _httpClient.GetByteArrayAsync(url).ConfigureAwait(false);
            var filePath = $"plate_{Guid.NewGuid()}.jpg";

            System.IO.File.WriteAllBytes(filePath, fileBytes);
            var result = ShellHelper.Bash($"alpr --json -c eu {filePath}");
            System.IO.File.Delete(filePath);

            return result;
        }
    }
}
