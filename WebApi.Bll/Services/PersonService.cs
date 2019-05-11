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
    public class PersonService : IPersonService
    {
        private readonly VelhoContext velho;

        public PersonService(VelhoContext context)
        {
            velho = context;
        }

        public void DeletePerson(int personId)
        {
            velho.Populii.Remove(new Person { ID = personId });
            velho.SaveChanges();
        }

        public Person GetPerson(int personId)
        {
            return velho.Populii
                .Include(p => p.PersonMeetings)
                .ThenInclude(pm => pm.Meeting)
                .SingleOrDefault(p => p.ID == personId) ?? throw new EntityNotFoundException("Nem található ilyen személy!");
                
        }

        public IEnumerable<Person> GetPersons()
        {
            var persons = velho.Populii
                .Include(p => p.PersonMeetings)
                    .ThenInclude(pm => pm.Meeting)
                .ToList();

            return persons;
        }

        public Person InsertPerson(Person newPerson)
        {
            velho.Populii.Add(newPerson);

            velho.SaveChanges();

            return newPerson;
        }

        public void UpdatePerson(int personId, Person updatedPerson)
        {
            updatedPerson.ID = personId;
            var entry = velho.Attach(updatedPerson);
            entry.State = EntityState.Modified;
            velho.SaveChanges();
        }
    }
}
