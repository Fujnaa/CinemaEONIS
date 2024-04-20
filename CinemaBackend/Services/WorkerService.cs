using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Services
{
    public class WorkerService : IWorkerService
    {

        CinemaDatabaseContext _dbContext;

        public WorkerService(CinemaDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Worker>> GetWorkers()
        {
            try
            {
                return await _dbContext.Workers.Include(w => w.Movies).ToListAsync();

            } catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Worker> GetWorkerById(Guid workerId)
        {
            try
            {
                Worker? search = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == workerId);

                if (search == null)
                    throw new KeyNotFoundException();

                return search;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Worker> CreateWorker(Worker worker)
        {
            var createdWorker = await _dbContext.AddAsync(worker);

            await _dbContext.SaveChangesAsync();

            return createdWorker.Entity;
        }

        public async Task<Worker> UpdateWorker(Worker worker)
        {
            try
            {
                var toUpdate = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == worker.WorkerId);

                if(toUpdate == null)
                    throw new KeyNotFoundException();

                toUpdate.WorkerId = worker.WorkerId;
                toUpdate.WorkerName = worker.WorkerName;
                toUpdate.WorkerCity = worker.WorkerCity;
                toUpdate.WorkerEmailAdress = worker.WorkerEmailAdress;
                toUpdate.WorkerPhoneNumber = worker.WorkerPhoneNumber;
                toUpdate.WorkerSalary = worker.WorkerSalary;

                await _dbContext.SaveChangesAsync();

                return toUpdate;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteWorker(Guid workerId)
        {
            try
            {
                Worker? search = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == workerId);

                if (search == null)
                    throw new KeyNotFoundException();

                _dbContext.Workers.Remove(search);

                await _dbContext.SaveChangesAsync();

            } catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
