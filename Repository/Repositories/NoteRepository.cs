//using ELearning_App.Domain.Entities;
//using ELearning_App.Repository.GenericRepositories;
//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ELearning_App.Repository.Repositories
//{
//    public class NoteRepository : GenericRepository<Note>, INoteRepository
//    {
//        private IUnitOfWork unitOfWork { get; }
//        public NoteRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
//        {
//            unitOfWork = _unitOfWork;
//        }

//        public async Task<IEnumerable<Note>> GetNotesByStudentIdByLessonId(int studentId, int lessonId)
//        {
//            return await unitOfWork.Context.Notes
//                .Where(n => n.StudentId == studentId && n.LessonId == lessonId)
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<Note>> GetNotesByStudentId(int studentId)
//        {
//            return await unitOfWork.Context.Notes
//                .Where(n => n.StudentId == studentId)
//                .ToListAsync();
//        }
//        public async Task<bool> IsValidNoteId(int id)
//        {
//            return await IsValidFk(a => a.Id == id);
//        }
//    }
//}
