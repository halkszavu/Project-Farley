using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Bll.Exceptions;

namespace WebApi.Bll.Services
{
    public interface IPersonService
    {
        /// <summary>
        /// Gets a person with the specified Id
        /// </summary>
        /// <param name="personId">The Id of the person</param>
        /// <returns>The person with the specified Id</returns>
        Task<Person> GetPersonAsync(int personId);

        /// <summary>
        /// Gets the first person from the Database with the specified name
        /// </summary>
        /// <param name="personName">Name or part of the name of the required person</param>
        /// <returns>The person with the correct name</returns>
        Task<Person> GetFirstPersonAsync(string personName);

        /// <summary>
        /// Gets the first person with the given date as birthday of the person
        /// </summary>
        /// <param name="dateOfBirth">The presumed birhtday of the person</param>
        /// <returns>The person with the birthday specified</returns>
        Task<IEnumerable<Person>> GetPersonsAsync(DateTime dateOfBirth);

        /// <summary>
        /// Gets all people from the DB
        /// </summary>
        /// <returns>List of people</returns>
        Task<IEnumerable<Person>> GetPersonsAsync();

        /// <summary>
        /// Gets all the people with the given name
        /// </summary>
        /// <param name="personName">The name or part of the name of the people</param>
        /// <returns>List of people</returns>
        Task<IEnumerable<Person>> GetPeopleNameAsync(string personName);

        /// <summary>
        /// Inserts a person into the database, even with the same name, that already exists
        /// </summary>
        /// <param name="newPerson">The person to insert</param>
        /// <returns>The inserted person</returns>
        Task<Person> InsertSamePersonAsync(Person newPerson);

        /// <summary>
        /// Inserts a person into the database
        /// </summary>
        /// <param name="newPerson">The person to insert</param>
        /// <returns>The inserted person</returns>
        /// <exception cref="DuplicateEntityException"/>
        Task<Person> InsertPersonAsync(Person newPerson);

        /// <summary>
        /// Changes a given person to a new one
        /// </summary>
        /// <param name="personId">The Id for the person to change</param>
        /// <param name="updatedPerson">The person that will be saved in the DB</param>
        /// <returns></returns>
        Task UpdatePersonAsync(int personId, Person updatedPerson);

        /// <summary>
        /// Deletes a person from the Database
        /// </summary>
        /// <param name="personId">Id of the person to delete</param>
        void DeletePerson(int personId);
    }

}
