using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Meeting
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Notes { get; set; }

        public ICollection<PersonMeeting> PersonMeetings { get; } = new List<PersonMeeting>();
    }
}
