using System.Threading.Tasks;
using WebApi.Bll.Dtos;

namespace WebApi.Bll.Services
{
    public interface INoteService
    {
        Task<Note> InsertNoteAsync(Note newNote);
        Task UpdateNoteAsync(int noteId, Note updatedNote);
        Task<Note> GetNoteAsync(int personId);
    }
}
