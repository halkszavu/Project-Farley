using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Bll.Exceptions;
using WebApi.Bll.Extensions;
using WebApi.DAL;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public class PersonService : IPersonService
    {
        private readonly VelhoContext velho;

        public PersonService(VelhoContext context) => velho = context;

        public void DeletePerson(int personId)
        {
            velho.Populii.Remove(new Person { ID = personId });
            velho.SaveChanges();
        }

        public async Task<Person> GetFirstPersonAsync(string personName) => await velho.Populii.FirstOrDefaultAsync(p => p.Name.Contains(personName));

        public async Task<IEnumerable<Person>> GetPeopleNameAsync(string personName) => await velho.Populii.Include(p => p.Name.Contains(personName)).ToListAsync();

        public async Task<Person> GetPersonAsync(int personId) => await velho.Populii
            .Include(p => p.PersonMeetings)
                .ThenInclude(pm => pm.Meeting)
            .SingleOrDefaultAsync(p => p.ID == personId) ?? throw new EntityNotFoundException("Nem található ilyen személy!");

        public async Task<IEnumerable<Person>> GetPersonsAsync() => await velho.Populii.ToListAsync();

        public async Task<IEnumerable<Person>> GetPersonsAsync(DateTime dateOfBirth) => await velho.Populii.Include(p => p.DateOfBirth.Subtract(dateOfBirth) <= TimeSpan.FromSeconds(86400)).ToListAsync(); //NOTE: Nem fog SQL-ként lefutni, mindent a kódban fog megcsinálni. Lehet valahogy javítani?

        public async Task<Person> InsertPersonAsync(Person newPerson)
        {
            if (velho.Populii.FirstOrDefault(p => p.Name.Contains(newPerson.Name)) == null)
            {
                velho.Populii.Add(newPerson.NullId());
                await velho.SaveChangesAsync();
                return newPerson;
            }
            else
                throw new DuplicateEntityException("There is a person with the same/similar name");
        }

        public async Task<Person> InsertSamePersonAsync(Person newPerson)
        {
            velho.Populii.Add(newPerson.NullId());
            await velho.SaveChangesAsync();
            return newPerson;
        }

        public async Task UpdatePersonAsync(int personId, Person updatedPerson)
        {
            updatedPerson.ID = personId;
            var entry = velho.Attach(updatedPerson);
            entry.State = EntityState.Modified;
            await velho.SaveChangesAsync();
        }
    }
}
