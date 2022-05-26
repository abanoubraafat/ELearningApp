using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class ParentService : IParentService
    {
        private IParentRepository iParentRepository { get; }

        public ParentService(IParentRepository _iParentRepository)
        {
            iParentRepository = _iParentRepository;
        }

        public async Task<Parent> AddParent(Parent p)
        {
            return await iParentRepository.Add(p);
        }

        public async Task<Parent> UpdateParent(Parent p)
        {
            return await iParentRepository.Update(p);
        }

        public async Task<Parent> DeleteParent(int id)
        {
            return await iParentRepository.Delete(id);
        }

        public async Task<IEnumerable<Parent>> GetAllParents()
        {
            return await iParentRepository.GetAll().ToListAsync();
        }

        public async Task<Parent> GetParentById(int id)
        {
            return await iParentRepository.GetById(id);
        }

        public async Task<IEnumerable<Parent>> GetAllWithStudentWithCoursesWithGrades()
        {
            return await iParentRepository.GetAllWithStudentWithCoursesWithGrades()
                //.Select(p => new Parent
                //{
                //    Id = p.Id,
                //    emailAddress = p.emailAddress,
                //    ParentFirstName = p.ParentFirstName,
                //    ParentLastName = p.ParentLastName,
                //    ParentPhone = p.ParentPhone,
                //    Students = p.Students
                //    .Select(s => new Student 
                //    { 
                //        Id = s.Id,
                //        emailAddress = s.emailAddress,
                //        StudentFirstName = s.StudentFirstName,
                //        StudentLastName = s.StudentLastName,
                //        StudentPhone = s.StudentPhone,
                //        Courses = s.Courses,
                //        //Grades = s.Grades
                //    }).ToList()
                //})
                .ToListAsync();
        }

        public async Task<IEnumerable<Parent>> GetByIdWithStudentWithCoursesWithGrades(int id)
        {
            return await iParentRepository.GetByIdWithStudentWithCoursesWithGrades(id)
                //.Select(p => new Parent
                //{
                //    Id = p.Id,
                //    emailAddress = p.emailAddress,
                //    ParentFirstName = p.ParentFirstName,
                //    ParentLastName = p.ParentLastName,
                //    ParentPhone = p.ParentPhone,
                //    Students = p.Students
                //    .Select(s => new Student
                //    {
                //        Id = s.Id,
                //        emailAddress = s.emailAddress,
                //        StudentFirstName = s.StudentFirstName,
                //        StudentLastName = s.StudentLastName,
                //        StudentPhone = s.StudentPhone,
                //        Courses = s.Courses,
                //        //Grades = s.Grades
                //    }).ToList()
                //})
                .ToListAsync();
        }
    }
}
