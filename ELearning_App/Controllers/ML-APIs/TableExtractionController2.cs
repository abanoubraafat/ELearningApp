//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;

//namespace ELearning_App.Controllers.ML_APIs
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TableExtractionController2 : ControllerBase
//    {
//        [HttpPost]
//        public async Task<ActionResult> Post([FromForm]TableExtractionRequestDTO dto)
//        {
//			string s;
//			//if (dto.file.Length > 0)
//			//{
//				using (var ms = new MemoryStream())
//				{
//					dto.file.CopyTo(ms);
//					var fileBytes = ms.ToArray();
//					s = Convert.ToBase64String(fileBytes);
//					// act on the Base64 data
//				}
//			//}
//			var client = new HttpClient();
//			var request = new HttpRequestMessage
//			{
//				Method = HttpMethod.Post,
//				RequestUri = new Uri("https://extract-table-documentdev.p.rapidapi.com/extracttable"),
//				Headers =
//					{
//						{ "Pages", "1" },
//						{ "X-RapidAPI-Key", "fdacb2ce0bmshb0a36e2081822c8p1e7ae6jsn82387bc6a932" },
//						{ "X-RapidAPI-Host", "extract-table-documentdev.p.rapidapi.com" },
//					},
//				Content = new StringContent(s)
//				{
//					Headers =
//						{
//							ContentType = new MediaTypeHeaderValue("text/plain")
//						}
//				}
//			};
//			using (var response = await client.SendAsync(request))
//			{
//				response.EnsureSuccessStatusCode();
//				var body = await response.Content.ReadAsStringAsync();
//				Console.WriteLine(body);
//				return Ok(body);
//			}
			
//        }
//    }
//}
