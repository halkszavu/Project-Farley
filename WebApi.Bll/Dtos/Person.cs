using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Bll.Dtos
{
    public class PersonDto
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Person name is mandatory", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MartialState MartialState { get; set; }
        public SiblingState SiblingState { get; set; }
        [Required(ErrorMessage ="RowVersion is mandatory", AllowEmptyStrings = true)]
        public byte[] RowVersion { get; set; }

        public List<MeetingDto> Meetings { get; set; }
    }
}
