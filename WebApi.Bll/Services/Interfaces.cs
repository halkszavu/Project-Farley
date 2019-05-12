using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Bll.Services
{
    public interface IPersonService
    {
        Person GetPerson(int personId);
        Task<Person> GetPersonAsync(int personId);
        IEnumerable<Person> GetPersons();
        Person InsertPerson(Person newPerson);
        void UpdatePerson(int personId, Person updatedPerson);
        void DeletePerson(int personId);
    }

    public interface IMeetingService
    {
        Meeting CreateMeeting(int personId, Meeting meeting);
    }
}
