using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface IPersonService
    {
        Task<Person> GetPersonAsync(int personId);
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<Person> InsertPersonAsync(Person newPerson);
        Task UpdatePersonAsync(int personId, Person updatedPerson);
        void DeletePerson(int personId);
    }

    public interface IMeetingService
    {
        Task<Meeting> CreateMeetingAsync(int personId, Meeting meeting);
        Task<Meeting> GetMeetingAsync(int meetingId);
        Task<IEnumerable<Meeting>> GetMeetingsAsync();
        Task UpdateMeetingAsync(int meetingId, Meeting updatedMeeting);
    }

    public interface INoteService
    {
        Task<Note> InsertNoteAsync(Note newNote);
        Task UpdateNoteAsync(int noteId, Note updatedNote);
    }
}
