using System;

namespace WebApi.Entities
{
    public class Note
    {
        public int ID { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }

        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}
