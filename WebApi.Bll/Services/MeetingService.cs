using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WebApi.DAL;
using WebApi.Entities;
using WebApi.Bll.Exceptions;

namespace WebApi.Bll.Services
{
    class MeetingService : IMeetingService
    {
        private readonly VelhoContext velho;

        public MeetingService(VelhoContext context)
        {
            velho = context;
        }

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
    }
}
