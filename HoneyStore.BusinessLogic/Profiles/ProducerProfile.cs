using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class ProducerProfile: Profile
    {
        public ProducerProfile()
        {
            CreateMap<Producer, ProducerDto>().ReverseMap();
        }   
    }
}