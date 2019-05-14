using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Bll.Exceptions;
using WebApi.DAL;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly VelhoContext velho;

        public MeetingService(VelhoContext context) => velho = context;

        public Meeting CreateMeeting(int personId, Meeting meeting)
        {
            var person = velho.Populii
                .Include(p=>p.PersonMeetings)
                .SingleOrDefault(p => p.ID == personId) ?? throw new EntityNotFoundException("Nem található ilyen személy!");

            person.PersonMeetings.Add(new PersonMeeting()
            {
                Person = person,
                Meeting = meeting
            });

            velho.SaveChanges();
            return meeting;
        }

        public async Task<Meeting> CreateMeetingAsync(int personId, Meeting meeting)
        {
            var person = await velho.Populii
                .Include(p => p.PersonMeetings)
                .SingleOrDefaultAsync(p => p.ID == personId) ?? throw new EntityNotFoundException("Nem található ilyen személy!");

            person.PersonMeetings.Add(new PersonMeeting()
            {
                Person = person,
                Meeting = meeting
            });

            await velho.SaveChangesAsync();
            return meeting;
        }

        public async Task<Meeting> GetMeetingAsync(int meetingId) => await velho.Meetings.SingleOrDefaultAsync(m => m.ID == meetingId) ?? throw new EntityNotFoundException("Nem található ilyen találkozás!");

        public async Task<IEnumerable<Meeting>> GetMeetingsAsync() => await velho.Meetings.ToListAsync();

        public async Task<IEnumerable<Meeting>> GetMeetingsAsync(Predicate<DateTime> predicate) => await velho.Meetings.Where(m => predicate(m.Date)).ToListAsync();

        public async Task UpdateMeetingAsync(int meetingId, Meeting updatedMeeting)
        {
            updatedMeeting.ID = meetingId;
            var entry = velho.Attach(updatedMeeting);
            entry.State = EntityState.Modified;
            await velho.SaveChangesAsync();
        }
    }
}
