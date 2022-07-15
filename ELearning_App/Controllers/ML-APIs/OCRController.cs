using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Linq;
namespace ELearning_App.Controllers.ML_APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController : ControllerBase
    {

        // Add your Computer Vision subscription key and endpoint
        static string subscriptionKey = "801adcabb2984e64aab3c0c8fb883e49";
        static string endpoint = "https://polapolaprojectlampit.cognitiveservices.azure.com/";

        private const string READ_TEXT_URL_IMAGE = "https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/ComputerVision/Images/printed_text.jpg";
        

        private static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        private async Task<List<string>> ReadFileUrl(ComputerVisionClient client, string urlFile, IFormFile file)
        {
            List<string> list = new();
            // Read text from URL
            //var textHeaders = await client.ReadAsync(urlFile);
            var textHeaders = await client.ReadInStreamAsync(file.OpenReadStream());
            // After the request, get the operation location (operation ID)
            string operationLocation = textHeaders.OperationLocation;
            Thread.Sleep(2000);

            // Retrieve the URI where the extracted text will be stored from the Operation-Location header.
            // We only need the ID and not the full URL
            const int numberOfCharsInOperationId = 36;
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

            // Extract the text
            ReadOperationResult results;
            //Console.WriteLine($"Extracting text from file {Path.GetFileName(urlFile)}...");
            //Console.WriteLine();
            do
            {
                results = await client.GetReadResultAsync(Guid.Parse(operationId));
            }
            while ((results.Status == OperationStatusCodes.Running ||
                results.Status == OperationStatusCodes.NotStarted));

            // Display the found text.
            //Console.WriteLine();
            var textUrlFileResults = results.AnalyzeResult.ReadResults;
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    //Console.WriteLine(line.Text);
                    list.Add(line.Text);
                }
            }
            //Console.WriteLine();
            return list;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] TableExtractionRequestDTO dto)
        {
            try
            {
                ComputerVisionClient client = Authenticate(endpoint, subscriptionKey);

                // Extract text (OCR) from a URL image using the Read API
                var list = await ReadFileUrl(client, READ_TEXT_URL_IMAGE, dto.file);
                return Ok(list);
            }
            catch(Exception)
            {
                return BadRequest("Invalid request");
            }
        }

    }
}
