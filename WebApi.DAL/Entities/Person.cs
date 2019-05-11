using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MartialState MartialState { get; set; }
        public SiblingState SiblingState { get; set; }

        public ICollection<PersonMeeting> PersonMeetings { get; } = new List<PersonMeeting>();
    }
}
