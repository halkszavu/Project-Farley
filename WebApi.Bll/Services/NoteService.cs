using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.DAL;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public class NoteService : INoteService
    {
        private readonly VelhoContext velho;

        public NoteService(VelhoContext context) => velho = context;

        public async Task<Note> InsertNoteAsync(Note newNote)
        {
            velho.Notes.Add(newNote);
            await velho.SaveChangesAsync();
            return newNote;
        }

        public async Task UpdateNoteAsync(int noteId, Note updatedNote)
        {
            updatedNote.ID = noteId;
            var entry = velho.Attach(updatedNote);
            entry.State = EntityState.Modified;
            await velho.SaveChangesAsync();
        }
    }
}
