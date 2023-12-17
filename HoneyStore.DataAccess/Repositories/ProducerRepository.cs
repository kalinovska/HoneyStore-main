using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class ProducerRepository : BaseRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(StoreDbContext context) : base(context)
        {

        }

        public override async Task UpdateAsync(int id, Producer producer)
        {
            var producerFromDb = await _context.Producers.FirstOrDefaultAsync(p => p.Id == id);

            producerFromDb.Name = producer.Name;
            producerFromDb.Description = producer.Description;

            _context.Producers.Update(producerFromDb);
        }
    }
}