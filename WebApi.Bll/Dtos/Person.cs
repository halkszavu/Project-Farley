using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Bll.Dtos
{
    public class Person
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Person name is mandatory", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MartialState MartialState { get; set; }
        public SiblingState SiblingState { get; set; }

        public List<Meeting> Meetings { get; set; }
    }
}
