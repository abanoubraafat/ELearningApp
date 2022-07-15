using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_App.Controllers.ML_APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleTranslateController : ControllerBase
    {
        [HttpGet("get-supported-languages")]
        public async Task<ActionResult> GetSupportedLanguages()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2/languages?target=en"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
                        { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<GetSupportedLanguagesResponseDTO>(body);
                    return Ok(json.data);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("translate")]
        public async Task<ActionResult> Translate(TranslateRequestDTO dto)
        {
            try
            {
                var client = new HttpClient();
                var dict = new Dictionary<string, string>
                        {
                            { "q", dto.TextToBeTranslated },
                            { "target", dto.TargetLang },
                        };
                if (dto.SourceLang != null) dict.Add("source", dto.SourceLang);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
                        { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
                    },
                    Content = new FormUrlEncodedContent(dict),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<TranslateResponseDTO>(body);
                    return Ok(json.data.Translations[0]);
                }
            }
            catch(System.Net.Http.HttpRequestException)
            {
                return BadRequest("Invalid targetLang or sourceLang");
            }
            catch (Exception)
            {
                return BadRequest("Invalid request");
            }
        }
        [HttpPost("detect")]
        public async Task<ActionResult> Detect()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2/detect"),
                Headers =
                    {
                        { "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
                        { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
                    },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "q", "English is hard, but detectably so" },
                    }),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return Ok(body);
            }
        }
    }
}
