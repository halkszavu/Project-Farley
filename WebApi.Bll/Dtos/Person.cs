using System;
using WebApi.Entities;

namespace WebApi.Bll.Dtos
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MartialState MartialState { get; set; }
        public SiblingState SiblingState { get; set; }
    }
}
