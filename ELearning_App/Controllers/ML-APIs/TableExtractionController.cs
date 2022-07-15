using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_App.Controllers.ML_APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableExtractionController : ControllerBase
    {
        
        private static readonly string endpoint = "https://lampit-table-extraction.cognitiveservices.azure.com/";
        private static readonly string apiKey = "2f6ad4bfd4a941539806fed8e78d5795";
        private static readonly AzureKeyCredential credential = new AzureKeyCredential(apiKey);
        private static readonly FormRecognizerClient recognizerClient = new FormRecognizerClient(new Uri(endpoint), credential);
        private static FormRecognizerClient AuthenticateClient()
        {
            var credential = new AzureKeyCredential(apiKey);
            var client = new FormRecognizerClient(new Uri(endpoint), credential);
            return client;
        }

        private async Task<List<string>> RecognizeContent(FormRecognizerClient recognizerClient, IFormFile file)
        {
            //RecognizeContentOptions recognizeContentOptions = new RecognizeContentOptions();
            //recognizeContentOptions.Language = FormRecognizerLanguage.En;
            List<string> tables = new();
            FormPageCollection formPages = await recognizerClient
            .StartRecognizeContent(file.OpenReadStream()/*, recognizeContentOptions*/)
            .WaitForCompletionAsync();

            foreach (FormPage page in formPages)
            {
                for (int i = 0; i < page.Tables.Count; i++)
                {
                    FormTable table = page.Tables[i];
                    Console.WriteLine($"Table {i} has {table.RowCount} rows and {table.ColumnCount} columns.");

                    //tables.Add($"Table {i} has {table.RowCount} rows and {table.ColumnCount} columns.");
                    foreach (FormTableCell cell in table.Cells)
                    {
                        Console.WriteLine($"    Cell ({cell.RowIndex}, {cell.ColumnIndex}) contains text: '{cell.Text}'.");

                        tables.Add($"{cell.RowIndex} {cell.ColumnIndex} {cell.Text}");
                    }
                }
            }
            return tables;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] TableExtractionRequestDTO dto)
        {
            try
            {
                var recognizeContent = RecognizeContent(recognizerClient, dto.file);
                Task.WaitAll(recognizeContent);
                return Ok(recognizeContent.Result);
            }
            catch(Exception)
            {
                return BadRequest("Invalid request");
            }
        }
    }
}
