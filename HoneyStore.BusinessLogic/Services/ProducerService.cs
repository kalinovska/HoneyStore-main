using AutoMapper;
using HoneyStore.BusinessLogic.Helpers;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.UnitOfWork;

namespace HoneyStore.BusinessLogic.Services
{
    public class ProducerService: BaseService, IProducerService
    {
        private readonly IMapper _mapper;

        public ProducerService(IUnitOfWork uow, IMapperFactory factory) : base(uow)
        {
            _mapper = factory.CreateMapper();
        }

        public async Task<ProducerDto> GetProducerAsync(int id)
        {
            var producerEntity = await _uow.Producers.GetAsync(id);

            var producerDto = _mapper.Map<Producer, ProducerDto>(producerEntity);

            return producerDto;
        }

        public async Task<ICollection<ProducerDto>> GetAllProducersAsync()
        {
            var producerEntities = await _uow.Producers.GetAllAsync();

            var producerDtos = _mapper.Map<ICollection<Producer>, ICollection<ProducerDto>>(producerEntities);

            return producerDtos;
        }

        public async Task AddProducerAsync(ProducerDto producer)
        {
            var producerEntity = _mapper.Map<ProducerDto, Producer>(producer);
            
            await _uow.Producers.AddAsync(producerEntity);

            await _uow.SaveAsync();
            producer.Id = producerEntity.Id;
        }

        public async Task RemoveProducerAsync(int id)
        {
            var producerEntity = await _uow.Producers.GetAsync(id);

            await _uow.Producers.RemoveAsync(producerEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateProducerAsync(int id, ProducerDto producer)
        {
            var producerEntity = _mapper.Map<ProducerDto, Producer>(producer);

            await _uow.Producers.UpdateAsync(id, producerEntity);

            await _uow.SaveAsync();
        }
    }
}