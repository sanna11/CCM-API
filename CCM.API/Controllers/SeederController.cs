﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CCM.Service.Interface;

namespace CCM.API.Controllers
{
    public class SeederController : BaseAPIController
    {
        private ITheatreService TheatreService { get; set; }

        private IMovieService MovieService { get; set; }

        private ITheatreSessionService SessionService { get; set; }

        private ITicketService TicketService { get; set; }

        public SeederController(ITheatreService service, IMovieService movieService, ITheatreSessionService sessionService,
            ITicketService ticketService)
        {
            TheatreService = service;
            this.MovieService = movieService;
            this.SessionService = sessionService;
            this.TicketService = ticketService;
        }

        [HttpGet("theatres")]
        public async Task<ActionResult<bool>> SeedTheatres() =>
            Ok(await TheatreService.SeedDataAsync().ConfigureAwait(true));


        [HttpGet("movies")]
        public async Task<ActionResult<bool>> SeedMovies() =>
           Ok(await MovieService.SeedDataAsync().ConfigureAwait(true));

        [HttpGet("session")]
        public async Task<ActionResult<bool>> SeedSessions() =>
           Ok(await SessionService.SeedDataAsync().ConfigureAwait(true));

        [HttpGet("tickets")]
        public async Task<ActionResult<bool>> SeedTickets() =>
           Ok(await TicketService.SeedDataAsync().ConfigureAwait(true));

    }
}