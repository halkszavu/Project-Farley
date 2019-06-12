using System;
using System.Collections.Generic;

namespace WebApi.Bll.Dtos
{
    public class MeetingDto
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Notes { get; set; }

        public List<PersonDto> People { get; set; }
    }
}
