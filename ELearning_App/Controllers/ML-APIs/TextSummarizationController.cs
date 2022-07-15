using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_App.Controllers.ML_APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextSummarizationController : ControllerBase
    {
        [HttpPost("Text")]
        public async Task<ActionResult> PostText(TextSummarizationTextRequestDTO dto)
        {
            try
            {
				var client = new HttpClient();
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://textanalysis-text-summarization.p.rapidapi.com/text-summarizer-text"),
					Headers =
					{
						{ "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
						{ "X-RapidAPI-Host", "textanalysis-text-summarization.p.rapidapi.com" },
					},
					Content = new FormUrlEncodedContent(new Dictionary<string, string>
					{
						{ "text", dto.Text},
						{ "sentnum", dto.Sentnum },
					}),
				};
				using (var response = await client.SendAsync(request))
				{
					response.EnsureSuccessStatusCode();
					var body = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<TextSummarizationResponseDTO>(body);
                    return Ok(body);
				}
			}
			catch (System.Net.Http.HttpRequestException)
			{
				return BadRequest("Not valid sentnum");
			}
			catch (Exception)
            {
				return BadRequest("Text field is empty");
            }
		}
        [HttpPost("URL")]
		public async Task<ActionResult> PostURL(TextSummarizationURLRequestDTO dto)
        {
			try
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://textanalysis-text-summarization.p.rapidapi.com/text-summarizer-url"),
					Headers =
					{
						{ "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
						{ "X-RapidAPI-Host", "textanalysis-text-summarization.p.rapidapi.com" },
					},
					Content = new FormUrlEncodedContent(new Dictionary<string, string>
					{
						{ "url", dto.Url },
						{ "sentnum", dto.Sentnum },
					}),
				};
				using (var response = await client.SendAsync(request))
				{
					response.EnsureSuccessStatusCode();
					var body = await response.Content.ReadAsStringAsync();
					var json = JsonConvert.DeserializeObject<TextSummarizationResponseDTO>(body);
					return Ok(json);
				}
			}
			catch (System.Net.Http.HttpRequestException)
			{
				return BadRequest("Not valid Url or sentnum");
			}
			catch (Exception)
			{
				return BadRequest("Url field is empty");
			}
		}
    }
}
