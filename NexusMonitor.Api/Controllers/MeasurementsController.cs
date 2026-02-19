using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusMonitor.Api.Data;
using NexusMonitor.Api.Models;

namespace NexusMonitor.Api.Controllers
{
    [ApiController]

    [Route("api/device/{deviceId}/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MeasurementsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetMeasurementsForDevice(int deviceId)
        {
            var query = _context.Measurements.Where(m => m.DeviceId == deviceId);
            var measurements = await query.ToListAsync();
            var measurementDtos = _mapper.Map<List<MeasurementDto>>(measurements);
            return Ok(measurementDtos);
        }
        [HttpPost]
        public async Task<ActionResult<MeasurementDto>> CreateMeasurementForDevice(int deviceId, [FromBody] CreateMeasurementDto createMeasurementDto)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            if (device == null)
            {
                return NotFound($"Device with ID {deviceId} not found.");
            }
            var measurement = _mapper.Map<Measurement>(createMeasurementDto);
            measurement.DeviceId = deviceId;
            measurement.Timestamp = DateTime.UtcNow;
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
            var measurementDto = _mapper.Map<MeasurementDto>(measurement);
            return CreatedAtAction(nameof(GetMeasurementsForDevice), new { deviceId }, measurementDto);
        }
    }
}
