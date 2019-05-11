using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Entities
{
    public class Note
    {
        public int ID { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }
    }
}
