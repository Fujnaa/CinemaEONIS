using AutoMapper;
using CinemaBackend.Models.DTOs.WorkerDTOs;
using CinemaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Worker")]
    public class WorkerController : ControllerBase
    {
        private IWorkerService _workerService;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerService workerService, IMapper mapper)
        {
            _workerService = workerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkerDto>>> GetWorkers()
        {
            try
            {
                List<Worker> workers = await _workerService.GetWorkers();

                if (workers == null || workers.Count == 0)
                    return NoContent();

                List<WorkerDto> workersDto = new List<WorkerDto>();

                foreach (var worker in workers)
                {
                    var workerDto = _mapper.Map<WorkerDto>(worker);
                    workersDto.Add(workerDto);
                }

                return Ok(workersDto);
            }catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            
            }

        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Worker")]
        public async Task<ActionResult<List<WorkerAdminDto>>> GetWorkersAdmin()
        {
            try
            {
                List<Worker> workers = await _workerService.GetWorkersAdmin();

                if (workers == null || workers.Count == 0)
                    return NoContent();

                List<WorkerAdminDto> workersDto = new List<WorkerAdminDto>();

                foreach (var worker in workers)
                {
                    var workerDto = _mapper.Map<WorkerAdminDto>(worker);
                    workersDto.Add(workerDto);
                }

                return Ok(workersDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }

        }

        [HttpGet("{workerId}")]
        public async Task<ActionResult<WorkerDto>> GetWorkerById(Guid workerId)
        {
            try
            {
                Worker worker = await _workerService.GetWorkerById(workerId);

                if (worker == null)
                    return NotFound();

                WorkerDto workerDto = _mapper.Map<WorkerDto>(worker);

                return Ok(workerDto);

            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("Email/{workerEmail}")]
        public async Task<ActionResult<WorkerDto>> GetWorkerByEmail(String workerEmail)
        {
            try
            {
                Worker worker = await _workerService.GetWorkerByEmail(workerEmail);

                if (worker == null)
                    return NotFound();

                WorkerDto workerDto = _mapper.Map<WorkerDto>(worker);

                return Ok(workerDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerCreateDto worker)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Worker toCreate = _mapper.Map<Worker>(worker);

                Worker createdWorker = await _workerService.CreateWorker(toCreate);

                return _mapper.Map<WorkerDto>(createdWorker);

            } catch(Exception ex)
            {

                throw new Exception(ex.Message, ex);
            
            }
        }

        [HttpPut]
        public async Task<ActionResult<WorkerDto>> UpdateWorker(WorkerUpdateDto worker)
        {
            try
            {
                Worker toUpdate = _mapper.Map<Worker>(worker);

                Worker updatedWorker = await _workerService.UpdateWorker(toUpdate);

                WorkerDto workerDto = _mapper.Map<WorkerDto>(updatedWorker);

                return Ok(workerDto);

            } catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWorker(Guid workerId)
        {
            try
            {

                await _workerService.DeleteWorker(workerId);
                return Ok();

            }   catch(Exception ex) 
            { 
            
                throw new Exception(ex.Message, ex);
            
            }
        }
    }
}
