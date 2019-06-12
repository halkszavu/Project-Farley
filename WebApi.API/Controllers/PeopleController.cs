using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Bll.Services;
using WebApi.Bll.Dtos;
using WebApi.Entities;

namespace WebApi.API.Controllers
{
    /// <summary>
    /// API Controller for multiple person entities in the database, gets and sets via IPersonService, using json
    /// </summary>
    [ApiVersion("1.0", Deprecated = true)]
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
        [ApiVersion("2.0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAsync() => mapper.Map<List<PersonDto>>(await personService.GetPersonsAsync()).ToList();

        //GET: api/People/name
        /// <summary>
        /// Returns a list of person whose name contains the name required
        /// </summary>
        /// <param name="name">The name or partial name of the people to get</param>
        /// <returns>List of person</returns>
        [ApiVersion("2.0")]
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAsync(string name) => mapper.Map<List<PersonDto>>(await personService.GetPeopleNameAsync(name));
    }
}
