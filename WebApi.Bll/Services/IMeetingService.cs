using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface IMeetingService
    {
        /// <summary>
        /// Inserts a new meeting into the database
        /// </summary>
        /// <param name="personId">The initial person of the meeting</param>
        /// <param name="meeting">The meeting to insert</param>
        /// <returns>Meeting</returns>
        Task<Meeting> CreateMeetingAsync(int personId, Meeting meeting);

        /// <summary>
        /// Gets a specified meeting from the database
        /// </summary>
        /// <param name="meetingId">The Id of the meeting</param>
        /// <returns>Meeting</returns>
        Task<Meeting> GetMeetingAsync(int meetingId);

        /// <summary>
        /// Gets a list of meeting with the specified time constraints
        /// </summary>
        /// <param name="predicate">The constraints of the time for the meetings</param>
        /// <returns>List of Meeting</returns>
        Task<IEnumerable<Meeting>> GetMeetingsAsync(Predicate<DateTime> predicate);

        /// <summary>
        /// Gets all the meeting from the database
        /// </summary>
        /// <returns>List of Meeting</returns>
        Task<IEnumerable<Meeting>> GetMeetingsAsync();

        /// <summary>
        /// Updates an existing meeting in the database
        /// </summary>
        /// <param name="meetingId">The Id of the meeting to update</param>
        /// <param name="updatedMeeting">The meeting to update to</param>
        Task UpdateMeetingAsync(int meetingId, Meeting updatedMeeting);
    }
}
