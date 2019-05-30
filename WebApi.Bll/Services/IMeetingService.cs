using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface IMeetingService
    {
        Task<Meeting> CreateMeetingAsync(int personId, Meeting meeting);
        Task<Meeting> GetMeetingAsync(int meetingId);
        Task<IEnumerable<Meeting>> GetMeetingsAsync(Predicate<DateTime> predicate);
        Task<IEnumerable<Meeting>> GetMeetingsAsync();
        Task UpdateMeetingAsync(int meetingId, Meeting updatedMeeting);
    }
}
