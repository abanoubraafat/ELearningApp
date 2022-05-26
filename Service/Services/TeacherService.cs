using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class TeacherService : ITeacherService
    {
        private ITeacherRepository iTeacherRepository { get; }

        public TeacherService(ITeacherRepository _iTeacherRepository)
        {
            iTeacherRepository = _iTeacherRepository;
        }

        public async Task<Teacher> AddTeacher(Teacher t)
        {
            return await iTeacherRepository.Add(t);
        }

        public async Task<Teacher> DeleteTeacher(int id)
        {
            return await iTeacherRepository.Delete(id);
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await iTeacherRepository.GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllWithCourses()
        {
            return await iTeacherRepository.GetAllWithCourses()
                .Select(t => new Teacher
                {
                    Id = t.Id,
                    EmailAddress = t.EmailAddress,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Phone = t.Phone,
                    Courses = t.Courses
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetByIdWithCourses(int id)
        {
            return await iTeacherRepository.GetByIdWithCourses(id)
                .Select(t => new Teacher
                {
                    Id = t.Id,
                    EmailAddress = t.EmailAddress,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Phone = t.Phone,
                    Courses = t.Courses
                })
                .ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await iTeacherRepository.GetById(id);
        }

        public async Task<Teacher> UpdateTeacher(Teacher t)
        {
            return await iTeacherRepository.Update(t);
        }
    }
}
