using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Bll.Services;
using WebApi.DAL;
using WebApi.Entities;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        public PeopleController(IPersonService service, IMapper cartographer)
        {
            personService = service;
            mapper = cartographer;
        }

        // GET: api/People
        [HttpGet]
        public ActionResult< IEnumerable<Person> >Get()
        {
            return mapper.Map<List<Person>>(personService.GetPersons()).ToList();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            return mapper.Map<Person>(personService.GetPerson(id));
        }

        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person person)
        {
            var created = personService
                .InsertPerson(mapper.Map<Entities.Person>(person));

            return CreatedAtAction(
                        nameof(Get),
                        new { id = created.ID },
                        mapper.Map<Person>(created)
                );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            personService.UpdatePerson(id, mapper.Map<Entities.Person>(person));
            return NoContent();
        }
    }
}
