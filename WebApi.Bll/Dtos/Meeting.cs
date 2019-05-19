using System;
using System.Collections.Generic;

namespace WebApi.Bll.Dtos
{
    public class Meeting
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Notes { get; set; }

        public List<Person> People { get; set; }
    }
}
