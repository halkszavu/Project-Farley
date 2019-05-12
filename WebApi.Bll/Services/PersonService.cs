using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Bll.Exceptions;
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

        public async Task<Person> GetPersonAsync(int personId) => await velho.Populii.SingleOrDefaultAsync(p => p.ID == personId) ?? throw new EntityNotFoundException("Nem található ilyen személy!");

        public async Task<IEnumerable<Person>> GetPersonsAsync() => await velho.Populii.ToListAsync();

        public async Task<Person> InsertPersonAsync(Person newPerson)
        {
            velho.Populii.Add(newPerson);
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
