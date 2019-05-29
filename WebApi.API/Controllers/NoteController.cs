using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Bll.Dtos;
using WebApi.Bll.Services;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">INoteService service for the layer below</param>
        /// <param name="cartographer">IMapper mapping the Bll and DAL layers</param>
        public NoteController(INoteService service, IMapper cartographer)
        {
            noteService = service;
            mapper = cartographer;
        }

        // GET: api/Note/5
        /// <summary>
        /// Gets a specified Note with the specified person
        /// </summary>
        /// <param name="id">Integer personId</param>
        /// <returns>The node associated with the person</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetAsync(int id) => mapper.Map<Note>(await noteService.GetNoteAsync(id));

        // POST: api/Note
        /// <summary>
        /// Inserts a Note to the specified Person
        /// </summary>
        /// <param name="personId">Integer personId</param>
        /// <param name="note">The note for the person specified</param>
        [HttpPost]
        public async Task<ActionResult<Note>> PostAsync(int personId, [FromBody] Note note)
        {
            var created = await noteService
                  .InsertNoteAsync(mapper.Map<Entities.Note>(note));

            return CreatedAtAction(
                        nameof(GetAsync),
                        new { id = created.ID },
                        mapper.Map<Note>(created)
                );
        }

        // PUT: api/Note/5
        /// <summary>
        /// Updates the Note to the specified Person
        /// </summary>
        /// <param name="personId">Integer personId</param>
        /// <param name="note">The updated note</param>
        [HttpPut("{personId}")]
        public async Task<IActionResult> PutAsync(int personId, [FromBody] Note note)
        {
            await noteService.UpdateNoteAsync(personId, mapper.Map<Entities.Note>(note));
            return NoContent();
        }
    }
}
