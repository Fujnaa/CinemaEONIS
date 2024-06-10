using AutoMapper;
using CinemaBackend.Models.DTOs.ScreeningDTOs;
using CinemaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : ControllerBase
    {
        private IScreeningService _screeningService;
        private readonly IMapper _mapper;

        public ScreeningController(IScreeningService screeningService, IMapper mapper)
        {
            _screeningService = screeningService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScreeningDto>>> GetScreenings()
        {
            try
            {
                List<Screening> screenings = await _screeningService.GetScreenings();

                if (screenings == null || screenings.Count == 0)
                    return NoContent();

                List<ScreeningDto> screeningsDto = new List<ScreeningDto>();

                foreach(var screening in screenings)
                {
                    ScreeningDto screeningDto = _mapper.Map<ScreeningDto>(screening);
                    screeningsDto.Add(screeningDto);
                }

                return Ok(screeningsDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<List<ScreeningAdminDto>>> GetScreeningsAdmin()
        {
            try
            {
                List<Screening> screenings = await _screeningService.GetScreeningsAdmin();

                if (screenings == null || screenings.Count == 0)
                    return NoContent();

                List<ScreeningAdminDto> screeningsDto = new List<ScreeningAdminDto>();

                foreach (var screening in screenings)
                {
                    ScreeningAdminDto screeningDto = _mapper.Map<ScreeningAdminDto>(screening);
                    screeningsDto.Add(screeningDto);
                }

                return Ok(screeningsDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("{screeningId}")]
        public async Task<ActionResult<ScreeningDto>> GetScreeningById(Guid screeningId)
        {
            try
            {
                Screening screening = await _screeningService.GetScreeningById(screeningId);

                if (screening == null)
                    return NotFound();

                ScreeningDto screeningDto = _mapper.Map<ScreeningDto>(screening);

                return Ok(screeningDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("Date/{screeningDate}")]
        public async Task<ActionResult<ScreeningDto>> GetScreeningByDate(DateOnly screeningDate)
        {
            try
            {
                Screening screening = await _screeningService.GetScreeningByDate(screeningDate);

                if (screening == null)
                    return NotFound();

                ScreeningDto screeningDto = _mapper.Map<ScreeningDto>(screening);

                return Ok(screeningDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<ScreeningDto>> CreateScreening(ScreeningCreateDto screening)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Screening toCreate = _mapper.Map<Screening>(screening);

                Screening createdScreening =  await _screeningService.CreateScreening(toCreate);

                return _mapper.Map<ScreeningDto>(createdScreening);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }

        [HttpPut]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<ScreeningDto>> UpdateScreening(ScreeningUpdateDto screening)
        {
            try
            {
                Screening toUpdate = _mapper.Map<Screening>(screening);

                Screening updatedScreening = await _screeningService.UpdateScreening(toUpdate);

                ScreeningDto screeningDto = _mapper.Map<ScreeningDto>(updatedScreening);

                return Ok(screeningDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult> DeleteScreening(Guid screeningId)
        {
            try
            {

                await _screeningService.DeleteScreening(screeningId);
                return Ok();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);

            }
        }
    }
}
