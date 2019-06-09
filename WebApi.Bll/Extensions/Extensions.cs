using WebApi.Entities;

namespace WebApi.Bll.Extensions
{
    public static class PersonExtensions
    {
        public static Person NullId(this Person person) => new Person
        {
            Name = person.Name,
            DateOfBirth = person.DateOfBirth,
            MartialState = person.MartialState,
            SiblingState = person.SiblingState
        };
    }
}
