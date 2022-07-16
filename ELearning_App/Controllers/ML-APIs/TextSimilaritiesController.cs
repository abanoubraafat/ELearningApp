using Domain.DTOs.ML_APIs_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ELearning_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextSimilaritiesController : ControllerBase
    {
        #region Get
        //[HttpGet]
        //public async Task<ActionResult> Get()
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://twinword-text-similarity-v1.p.rapidapi.com/similarity/?text1=The%20hippocampus%20is%20a%20major%20component%20of%20the%20brains%20of%20humans%20and%20other%20vertebrates.%20It%20belongs%20to%20the%20limbic%20system%20and%20plays%20important%20roles%20in%20the%20consolidation%20of%20information%20from%20short-term%20memory%20to%20long-term%20memory%20and%20spatial%20navigation.%20Humans%20and%20other%20mammals%20have%20two%20hippocampi%2C%20one%20in%20each%20side%20of%20the%20brain.%20The%20hippocampus%20is%20a%20part%20of%20the%20cerebral%20cortex%3B%20and%20in%20primates%20it%20is%20located%20in%20the%20medial%20temporal%20lobe%2C%20underneath%20the%20cortical%20surface.%20It%20contains%20two%20main%20interlocking%20parts%3A%20Ammon's%20horn%20and%20the%20dentate%20gyrus.&text2=An%20important%20part%20of%20the%20brains%20of%20humans%20and%20other%20vertebrates%20is%20the%20hippocampus.%20It's%20part%20of%20the%20limbic%20system%20and%20moves%20information%20from%20short-term%20to%20long-term%20memory.%20It%20also%20helps%20us%20move%20around.%20Humans%20and%20other%20mammals%20have%20two%20hippocampi%2C%20one%20on%20each%20side.%20The%20hippocampus%20is%20a%20part%20of%20the%20cerebral%20cortex%3B%20and%20in%20primates%20it%20is%20found%20in%20the%20medial%20temporal%20lobe%2C%20beneathe%20the%20cortical%20surface.%20It%20has%20two%20main%20interlocking%20parts%3A%20Ammon's%20horn%20and%20the%20dentate%20gyrus."),
        //        Headers =
        //                {
        //                    { "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
        //                    { "X-RapidAPI-Host", "twinword-text-similarity-v1.p.rapidapi.com" },
        //                },
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();
        //        //Console.WriteLine(body);
        //        return Ok(body);
        //    }  
        //} 
        #endregion

        [HttpPost]
        public async Task<ActionResult> Post(TextSimilarityRequestDTO dto)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://twinword-text-similarity-v1.p.rapidapi.com/similarity/"),
                    Headers =
                        {
                            { "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
                            { "X-RapidAPI-Host", "twinword-text-similarity-v1.p.rapidapi.com" },
                        },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "text1", dto.text1 },
                    { "text2", dto.text2 },
                }),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<TextSimilarityResponseDTO>(body);
                    return Ok(json);
                }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest("Invalid request");
            }
        }
    }
}
