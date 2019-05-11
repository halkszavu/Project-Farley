using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //private readonly VelhoContext context;
        private readonly IPersonService personService;

        public PeopleController(IPersonService service)
        {
            personService = service;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return personService.GetPersons();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = personService.GetPerson(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        //// PUT: api/People/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPerson(int id, Person person)
        //{
        //    if (id != person.ID)
        //    {
        //        return BadRequest();
        //    }

        //    context.Entry(person).State = EntityState.Modified;

        //    try
        //    {
        //        await context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PersonExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/People
        //[HttpPost]
        //public async Task<ActionResult<Person>> PostPerson(Person person)
        //{
        //    context.Populii.Add(person);
        //    await context.SaveChangesAsync();

        //    return CreatedAtAction("GetPerson", new { id = person.ID }, person);
        //}

        //// DELETE: api/People/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Person>> DeletePerson(int id)
        //{
        //    var person = await context.Populii.FindAsync(id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    context.Populii.Remove(person);
        //    await context.SaveChangesAsync();

        //    return person;
        //}
    }
}
