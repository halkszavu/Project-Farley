using System;

namespace WebApi.Bll.Dtos
{
    public class NoteDto
    {
        public int ID { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }

        public int PersonID { get; set; }
        public PersonDto Person { get; set; }
    }
}
