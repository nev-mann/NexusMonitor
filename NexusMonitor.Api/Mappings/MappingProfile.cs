using AutoMapper;
using NexusMonitor.Api.Models;

namespace NexusMonitor.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Zapis
            CreateMap<CreateDeviceDto, Device>();
            // Odczyt
            CreateMap<Device, CreateDeviceDto>();

            CreateMap<CreateMeasurementDto, Measurement>();
            CreateMap<Measurement, CreateMeasurementDto>();
            CreateMap<Measurement, MeasurementDto>();

            CreateMap<RegisterUserDto, UserAccount>();

            CreateMap<UpdateDeviceDto, Device>();

            CreateMap<Device, DeviceDto>();
        }
    }
}