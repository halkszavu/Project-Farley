using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Bll.Exceptions;
using WebApi.DAL;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public class NoteService : INoteService
    {
        private readonly VelhoContext velho;

        public NoteService(VelhoContext context) => velho = context;

        public async Task<Note> GetNoteAsync(int personId) => await velho.Notes.FirstOrDefaultAsync(n => n.PersonID == personId) ?? throw new EntityNotFoundException("Nem található ilyen jegyzet");

        public async Task<Note> InsertNoteAsync(Note newNote)
        {
            velho.Notes.Add(newNote);
            await velho.SaveChangesAsync();
            return newNote;
        }

        public async Task UpdateNoteAsync(int personId, Note updatedNote)
        {
            updatedNote.PersonID = personId;
            var entry = velho.Attach(updatedNote);
            entry.State = EntityState.Modified;
            await velho.SaveChangesAsync();
        }
    }
}
