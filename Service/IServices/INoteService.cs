using ELearning_App.Domain.Entities;

namespace ELearning_App.Service.IServices
{
    public interface INoteService
    {
        Task<Note> AddNote(Note g);
        Task<Note> UpdateNote(Note g);
        Task<Note> DeleteNote(int id);
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNoteById(int id);
    }
}
