using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface IPersonService
    {
        Task<Person> GetPersonAsync(int personId);
        Task<Person> GetFirstPersonAsync(string personName);
        Task<IEnumerable<Person>> GetPersonsAsync(DateTime dateOfBirth);
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<Person> InsertPersonAsync(Person newPerson);
        Task UpdatePersonAsync(int personId, Person updatedPerson);
        void DeletePerson(int personId);
    }

    public interface IMeetingService
    {
        Task<Meeting> CreateMeetingAsync(int personId, Meeting meeting);
        Task<Meeting> GetMeetingAsync(int meetingId);
        Task<IEnumerable<Meeting>> GetMeetingsAsync(Predicate<DateTime> predicate);
        Task<IEnumerable<Meeting>> GetMeetingsAsync();
        Task UpdateMeetingAsync(int meetingId, Meeting updatedMeeting);
    }

    public interface INoteService
    {
        Task<Note> InsertNoteAsync(Note newNote);
        Task UpdateNoteAsync(int noteId, Note updatedNote);
        Task<Note> GetNoteAsync(int personId);
    }
}
