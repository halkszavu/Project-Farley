using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface IPersonService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        Task<Person> GetPersonAsync(int personId);
        Task<Person> GetFirstPersonAsync(string personName);
        Task<IEnumerable<Person>> GetPersonsAsync(DateTime dateOfBirth);
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<IEnumerable<Person>> GetPeopleNameAsync(string personName);
        Task<Person> InsertSamePersonAsync(Person newPerson);
        Task<Person> InsertPersonAsync(Person newPerson);
        Task UpdatePersonAsync(int personId, Person updatedPerson);
        void DeletePerson(int personId);
    }

}
