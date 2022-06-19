//using ELearning_App.Domain.Entities;
//using ELearning_App.Repository.GenericRepositories;
//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ELearning_App.Repository.Repositories
//{
//    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
//    {
//        private IUnitOfWork unitOfWork { get; }
//        public AnnouncementRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
//        {
//            unitOfWork = _unitOfWork;
//        }

//        public async Task<bool> IsValidAnnouncementId(int id)
//        {
//            return await IsValidFk(a=> a.Id == id);
//        }

//        //public IQueryable<Announcement> GetByIdWithCourses(out int C_id, int id)
//        //{
//        //    C_id = unitOfWork.Context.Announcements.Include(a => a.Courses.Select(c => c.CourseId));
//        //    return unitOfWork.Context.Announcements.Where(a => a.AnnouncementId == id);
//        //}
//    }
//}
