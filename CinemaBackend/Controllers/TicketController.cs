﻿using AutoMapper;
using CinemaBackend.Models.DTOs.TicketDTOs;
using CinemaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private ITicketService _ticketService;
        private ICustomerService _customerService;
        private readonly IMapper _mapper;

        public TicketController(ITicketService ticketService, IMapper mapper, ICustomerService customerService)
        {
            _ticketService = ticketService;
            _mapper = mapper;
            _customerService = customerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<TicketDto>>> GetTickets()
        {
            try
            {
                List<Ticket> tickets = await _ticketService.GetTickets();

                if (tickets == null || tickets.Count == 0)
                    return NoContent();

                List<TicketDto> ticketsDto = new List<TicketDto>();

                foreach(var  ticket in tickets)
                {
                    var ticketDto = _mapper.Map<TicketDto>(ticket);
                    ticketsDto.Add(ticketDto);
                }

                return Ok(ticketsDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<List<TicketAdminDto>>> GetTicketsAdmin()
        {
            try
            {
                List<Ticket> tickets = await _ticketService.GetTickets();

                if (tickets == null || tickets.Count == 0)
                    return NoContent();

                List<TicketAdminDto> ticketsDto = new List<TicketAdminDto>();

                foreach (var ticket in tickets)
                {
                    var ticketDto = _mapper.Map<TicketAdminDto>(ticket);
                    ticketsDto.Add(ticketDto);
                }

                return Ok(ticketsDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("{ticketId}")]
        public async Task<ActionResult<TicketDto>> GetTicketById(Guid ticketId)
        {
            try
            {
                Ticket ticket = await _ticketService.GetTicketById(ticketId);

                if (ticket == null)
                    return NotFound();

                TicketDto ticketDto = _mapper.Map<TicketDto>(ticket);

                return Ok(ticketDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket(TicketCreateDto ticket)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Ticket toCreate = new Ticket();

                toCreate.TicketPrice = ticket.TicketPrice;
                toCreate.TicketSeat = ticket.TicketSeat;
                toCreate.TicketType = ticket.TicketType;
                toCreate.CustomerId = _customerService.GetCustomerByEmail(ticket.CustomerEmail!).Result.CustomerId;
                toCreate.ScreeningId = ticket.ScreeningId;

                Ticket createdTicket = await _ticketService.CreateTicket(toCreate);

                return _mapper.Map<TicketDto>(createdTicket);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }

        [HttpPut]
        public async Task<ActionResult<TicketDto>> UpdateTicket(TicketUpdateDto ticket)
        {
            try
            {
                Ticket toUpdate = _mapper.Map<Ticket>(ticket);

                Ticket updatedTicket = await _ticketService.UpdateTicket(toUpdate);

                TicketDto ticketDto = _mapper.Map<TicketDto>(updatedTicket);

                return Ok(ticketDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTicket(Guid ticketId)
        {
            try
            {

                await _ticketService.DeleteTicket(ticketId);
                return Ok();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }
    }
}
