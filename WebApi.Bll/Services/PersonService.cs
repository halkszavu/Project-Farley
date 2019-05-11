using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Bll.Exceptions;
using WebApi.DAL;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    class PersonService : IPersonService
    {
        private readonly VelhoContext velho;

        public PersonService(VelhoContext context)
        {
            velho = context;
        }

        public Person GetPerson(int personId)
        {
            return velho.Populii
                .Include(p => p.PersonMeetings)
                .ThenInclude(pm => pm.Meeting)
                .SingleOrDefault(p => p.ID == personId) ?? throw new EntityNotFoundException("Nem található ilyen személy!");
                
        }
    }
}
