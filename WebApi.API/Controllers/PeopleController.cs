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
    /// API Controller for multiple person entities in the database, gets and sets via IPersonService, using json
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
        public async Task<ActionResult<IEnumerable<Person>>> GetAsync() => mapper.Map<List<Person>>(await personService.GetPersonsAsync()).ToList();
    }
}
