using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository iAnnouncementRepository;

        public AnnouncementService(IAnnouncementRepository _iAnnouncementRepository)
        {
            iAnnouncementRepository = _iAnnouncementRepository;
        }
        public async Task<Announcement> AddAnnouncement(Announcement g)
        {
            return await iAnnouncementRepository.Add(g);
        }

        public async Task<Announcement> DeleteAnnouncement(int id)
        {
            return await iAnnouncementRepository.Delete(id);
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncements()
        {
            return await iAnnouncementRepository.GetAll().ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementById(int id)
        {
            return await iAnnouncementRepository.GetById(id);
        }

        //public async Task<IEnumerable<Announcement>> GetByIdWithCourses(int id)
        //{
        //    return await iAnnouncementRepository.GetByIdWithCourses(id).ToListAsync();
        //}

        public async Task<Announcement> UpdateAnnouncement(Announcement g)
        {
            return await iAnnouncementRepository.Update(g);
        }
    }
}
