using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface INoteService
    {
        /// <summary>
        /// Inserts a new note to the Database
        /// </summary>
        /// <param name="newNote">The note to insert</param>
        /// <returns>The inserted note</returns>
        Task<Note> InsertNoteAsync(Note newNote);

        /// <summary>
        /// Updates a note in the Database
        /// </summary>
        /// <param name="noteId">The Id of the note</param>
        /// <param name="updatedNote">The note to update to</param>
        /// <returns>The updated note</returns>
        Task UpdateNoteAsync(int noteId, Note updatedNote);

        /// <summary>
        /// Gets a note from a person
        /// </summary>
        /// <param name="personId">The id of the person</param>
        /// <returns>Note from the person</returns>
        Task<Note> GetNoteAsync(int personId);
    }
}
