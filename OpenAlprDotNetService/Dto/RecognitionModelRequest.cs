using System;
using System.ComponentModel.DataAnnotations;

namespace OpenAlprDotNetService.Dto
{
    public class RecognitionModelRequest
    {
        [Required]
        public string Base64String { get; set; }

        [Required]
        public string ConfigFile { get; set; }
    }
}