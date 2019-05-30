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
    /// API Controller for single person entities in the database, gets and sets via IPersonService, using json
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">IPersonService service for the layer below</param>
        /// <param name="cartographer">IMapper mapping the Bll and DAL layers</param>
        public PersonController(IPersonService service, IMapper cartographer)
        {
            personService = service;
            mapper = cartographer;
        }

        // GET: api/Person/byId5
        /// <summary>
        /// Gets a person from the DB by Id
        /// </summary>
        /// <param name="id">Integer personId</param>
        /// <returns>Person instance</returns>
        [HttpGet("byId{id}")]
        public async Task<ActionResult<Person>> GetAsync(int id) => mapper.Map<Person>(await personService.GetPersonAsync(id));

        // GET: api/Person/byNamename
        /// <summary>
        /// Gets a person from the DB by name (if their name contains the name, it will return the first person it found)
        /// </summary>
        /// <param name="name">Name or part of the name of the person</param>
        /// <returns>Person instance</returns>
        [HttpGet("byName{name}")]
        public async Task<ActionResult<Person>> GetAsync(string name) => mapper.Map<Person>(await personService.GetFirstPersonAsync(name));

        //POST:api/Person
        /// <summary>
        /// Inserts a new person into the database
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Person to insert into the database</returns>
        [HttpPost]
        public async Task<ActionResult<Person>> PostAsync([FromBody] Person person)
        {
            var created = await personService
                .InsertPersonAsync(mapper.Map<Person>(person));

            return CreatedAtAction(
                        nameof(GetAsync),
                        new { id = created.ID },
                        mapper.Map<Person>(created)
                );
        }

        //POST:api/Person
        /// <summary>
        /// Inserts a new person into the database, even if a person with the same name exists.
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Person to insert into the database</returns>
        [HttpPost("force")]
        public async Task<ActionResult<Person>> ForcePostAsync([FromBody] Person person)
        {
            var created = await personService
                .InsertSamePersonAsync(mapper.Map<Person>(person));

            return CreatedAtAction(
                        nameof(GetAsync),
                        new { id = created.ID },
                        mapper.Map<Person>(created)
                );
        }

        //PUT:api/Person
        /// <summary>
        /// Updates an existing person in the id position
        /// </summary>
        /// <param name="id">Integer personId</param>
        /// <param name="person">Person to update to</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Person person)
        {
            await personService.UpdatePersonAsync(id, mapper.Map<Person>(person));
            return NoContent();
        }
    }
}
