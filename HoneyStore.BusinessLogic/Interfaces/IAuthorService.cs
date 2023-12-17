using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface IProducerService
    {
        Task<ProducerDto> GetProducerAsync(int id);

        Task<ICollection<ProducerDto>> GetAllProducersAsync();

        Task AddProducerAsync(ProducerDto producer);

        Task RemoveProducerAsync(int id);

        Task UpdateProducerAsync(int id, ProducerDto producer);
    }
}