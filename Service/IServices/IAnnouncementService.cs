using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface IAnnouncementService
    {
        Task<Announcement> AddAnnouncement(Announcement g);
        Task<Announcement> UpdateAnnouncement(Announcement g);
        Task<Announcement> DeleteAnnouncement(int id);
        Task<IEnumerable<Announcement>> GetAllAnnouncements();
        Task<Announcement> GetAnnouncementById(int id);
        //Task<IEnumerable<Announcement>> GetByIdWithCourses(int id);
    }
}
