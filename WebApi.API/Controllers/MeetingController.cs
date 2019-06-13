using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Bll.Dtos;
using WebApi.Bll.Services;
using WebApi.Entities;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MeetingController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IMeetingService meetingService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">IMeetingService service for the layer below</param>
        /// <param name="cartographer">IMapper mapping the Bll and DAL layers</param>
        public MeetingController(IMeetingService service, IMapper cartographer)
        {
            meetingService = service;
            mapper = cartographer;
        }

        // GET: api/Meeting
        /// <summary>
        /// Gets all the meetings (Meeting entities) from the database
        /// </summary>
        /// <returns>List of Meetings</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingDto>>> GetAsync() => mapper.Map<List<MeetingDto>>(await meetingService.GetMeetingsAsync()).ToList();

        // GET: api/Meeting/5
        /// <summary>
        /// Gets a meeting from the DB
        /// </summary>
        /// <param name="id">Integer meetingId</param>
        /// <returns>Meeting instance</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDto>> GetAsync(int id) => mapper.Map<MeetingDto>(await meetingService.GetMeetingAsync(id));

        // PUT: api/Meeting/5
        /// <summary>
        /// Updates the meeting in the id position
        /// </summary>
        /// <param name="id">Integer meetingId</param>
        /// <param name="meeting">Meeting to update to</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] MeetingDto meeting)
        {
            await meetingService.UpdateMeetingAsync(id, mapper.Map<Meeting>(meeting));
            return NoContent();
        }

        //POST: api/Meeting
        /// <summary>
        /// Creates a meeting with the person
        /// </summary>
        /// <param name="personId">The personId for the meeting attendee</param>
        /// <param name="meeting">The meeting</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MeetingDto> Post(int personId, [FromBody] MeetingDto meeting) => mapper.Map<MeetingDto>(meetingService.CreateMeetingAsync(personId, mapper.Map<Meeting>(meeting)));
    }
}
