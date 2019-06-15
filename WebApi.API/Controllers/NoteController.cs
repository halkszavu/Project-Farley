using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Bll.Dtos;
using WebApi.Bll.Services;
using WebApi.Entities;

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
        public async Task<ActionResult<NoteDto>> GetAsync(int id) => mapper.Map<NoteDto>(await noteService.GetNoteAsync(id));

        // POST: api/Note
        /// <summary>
        /// Inserts a Note to the specified Person
        /// </summary>
        /// <param name="personId">Integer personId</param>
        /// <param name="note">The note for the person specified</param>
        [HttpPost]
        public async Task<ActionResult<NoteDto>> PostAsync(int personId, [FromBody] NoteDto note)
        {
            var created = await noteService.InsertNoteAsync(mapper.Map<Note>(note));

            return CreatedAtAction(
                        nameof(GetAsync),
                        new { id = created.ID },
                        mapper.Map<NoteDto>(created)
                );
        }

        // PUT: api/Note/5
        /// <summary>
        /// Updates the Note to the specified Person
        /// </summary>
        /// <param name="personId">Integer personId</param>
        /// <param name="note">The updated note</param>
        [HttpPut("{personId}")]
        public async Task<IActionResult> PutAsync(int personId, [FromBody] NoteDto note)
        {
            await noteService.UpdateNoteAsync(personId, mapper.Map<Note>(note));
            return NoContent();
        }
    }
}
