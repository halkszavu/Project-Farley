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
    /// <summary>
    /// API Controller for person entities in the database, gets and sets via IPersonService, using json
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">IPersonService service for the layer below</param>
        /// <param name="cartographer">IMapper mapping the Bll and DAL layers</param>
        public PeopleController(IPersonService service, IMapper cartographer)
        {
            personService = service;
            mapper = cartographer;
        }

        // GET: api/People
        /// <summary>
        /// Gets all the people (Person entities) from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult< IEnumerable<Person> >Get()
        {
            return mapper.Map<List<Person>>(personService.GetPersons()).ToList();
        }

        /// <summary>
        /// Gets a person from the DB
        /// </summary>
        /// <param name="id">Integer personId</param>
        /// <returns>Person instance</returns>
        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            return mapper.Map<Person>(await personService.GetPersonAsync(id));
        }

        /// <summary>
        /// Inserts person into the database
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Person to insert into the database</returns>
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

        /// <summary>
        /// Updates the person in the id position
        /// </summary>
        /// <param name="id">Integer personId</param>
        /// <param name="person">Person to update to</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            personService.UpdatePerson(id, mapper.Map<Entities.Person>(person));
            return NoContent();
        }
    }
}
