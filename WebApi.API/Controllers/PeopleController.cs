using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Bll.Services;
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
        /// <returns>List of Persons</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return mapper.Map<List<Person>>(await personService.GetPersonsAsync()).ToList();
        }

        // GET: api/People/5
        /// <summary>
        /// Gets a person from the DB
        /// </summary>
        /// <param name="id">Integer personId</param>
        /// <returns>Person instance</returns>
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
        public async Task<ActionResult<Person>> Post([FromBody] Person person)
        {
            var created = await personService
                .InsertPersonAsync(mapper.Map<Entities.Person>(person));

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
        public async Task<IActionResult> Put(int id, [FromBody] Person person)
        {
            await personService.UpdatePersonAsync(id, mapper.Map<Entities.Person>(person));
            return NoContent();
        }
    }
}
