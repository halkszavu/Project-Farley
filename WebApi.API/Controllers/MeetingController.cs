﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Bll.Dtos;
using WebApi.Bll.Services;

namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService meetingService;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">IPersonService service for the layer below</param>
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
        public async Task<ActionResult<IEnumerable<Meeting>>> Get()
        {
            return mapper.Map<List<Meeting>>(await meetingService.GetMeetingsAsync()).ToList();
        }

        // GET: api/Meeting/5
        /// <summary>
        /// Gets a meeting from the DB
        /// </summary>
        /// <param name="id">Integer meetinId</param>
        /// <returns>meeting instance</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            return mapper.Map<Person>(await meetingService.GetMeetingAsync(id));
        }

        // PUT: api/Meeting/5
        /// <summary>
        /// Updates the meeting in the id position
        /// </summary>
        /// <param name="id">Integer meetingId</param>
        /// <param name="meeting">Meeting to update to</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Meeting meeting)
        {
            await meetingService.UpdateMeetingAsync(id, mapper.Map<Entities.Meeting>(meeting));
            return NoContent();
        }
    }
}