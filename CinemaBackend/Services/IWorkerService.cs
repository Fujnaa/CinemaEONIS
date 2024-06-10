
namespace CinemaBackend.Services
{
    public interface IWorkerService
    {

        Task<List<Worker>> GetWorkers();
        Task<List<Worker>> GetWorkersAdmin();
        Task<Worker> GetWorkerById(Guid workerId);
        Task<Worker> GetWorkerByEmail(String workerEmail);
        Task<Worker> CreateWorker(Worker worker);
        Task<Worker> UpdateWorker(Worker worker);
        Task DeleteWorker(Guid workerId);

    }
}
