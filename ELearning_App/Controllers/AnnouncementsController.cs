//using ELearning_App.Domain.Entities;
//using ELearning_App.Helpers;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Serilog;

//namespace ELearning_App.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AnnouncementsController : ControllerBase
//    {
//        private IAnnouncementService service { get; }

//        public AnnouncementsController(IAnnouncementService _service)
//        {
//            service = _service;
//            new Logger();
//        }

//        // GET: api/Announcementes
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
//        {
//            try
//            {
//                return Ok(await service.GetAllAnnouncements());
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: AnnouncementController , Action: GetAnnouncements , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // GET: api/Announcementes/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
//        {
//            try
//            {
//                if (service.GetByIdAsync(id) == null)
//                    return NotFound();
//                return Ok(await service.GetByIdAsync(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: AnnouncementController , Action: GetAnnouncement , Message: {ex.Message}");
//                return NotFound();
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // PUT: api/Announcementes/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutAnnouncement(int id,[FromBody] Announcement a)
//        {

//            try
//            {
//                if (id != a.Id)
//                {
//                    return BadRequest();
//                }
//                return Ok(await service.UpdateAnnouncement(a));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: AnnouncementController , Action: PutAnnouncement , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // POST: api/Announcementes
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Announcement>> PostAnnouncement(Announcement loginInfo)
//        {
//            try
//            {
//                return Ok(await service.AddAnnouncement(loginInfo));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: AnnouncementController , Action: PostAnnouncement , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        // DELETE: api/Announcementes/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteAnnouncement(int id)
//        {
//            try
//            {
//                return Ok(await service.DeleteAnnouncement(id));
//            }
//            catch (Exception ex)
//            {
//                Log.Error($"Controller: AnnouncementController , Action: DeleteAnnouncement , Message: {ex.Message}");
//                return StatusCode(500);
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }
//        // GET: api/Announcementes/5
//        //[HttpGet("Courses/{id}/Announcements/{id}")]
//        //public async Task<ActionResult<Announcement>> GetByIdWithCourses(int id)
//        //{
//        //    try
//        //    {
//        //        return Ok(await service.GetByIdWithCourses(id));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Log.Error($"Controller: AnnouncementController , Action: GetByIdWithCourses , Message: {ex.Message}");
//        //        return NotFound();
//        //    }
//        //    finally
//        //    {
//        //        Log.CloseAndFlush();
//        //    }
//        //}
//    }
//}
