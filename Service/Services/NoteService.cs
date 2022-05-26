using ELearning_App.Domain.Entities;
using ELearning_App.Repository.IRepositories;
using ELearning_App.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ELearning_App.Service.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository iNoteRepository;

        public NoteService(INoteRepository _iNoteRepository)
        {
            iNoteRepository = _iNoteRepository;
        }
        public async Task<Note> AddNote(Note g)
        {
            return await iNoteRepository.Add(g);
        }

        public async Task<Note> DeleteNote(int id)
        {
            return await iNoteRepository.Delete(id);
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await iNoteRepository.GetAll().ToListAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await iNoteRepository.GetById(id);
        }

        public async Task<Note> UpdateNote(Note g)
        {
            return await iNoteRepository.Update(g);
        }
    }
}
