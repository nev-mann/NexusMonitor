using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // To daje dostęp do ToListAsync, FindAsync itd.
using NexusMonitor.Api.Data;  // To daje dostęp do AppDbContext
using NexusMonitor.Api.Dtos;     

namespace NexusMonitor.Api.Controllers
{

    [ApiController]

    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private static Random random = new Random();

        public DeviceController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tymczasowa lista urządzeń, zastąpić później bazą danych
        private static readonly List<Device> _devices = new()
        {
            new Device { DeviceId = 1, DeviceName = "Device 1", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) },
            new Device { DeviceId = 2, DeviceName = "Device 2", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) },
            new Device { DeviceId = 3, DeviceName = "Device 3", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) }
        };

        // /api/device/1
        [HttpGet("{deviceId}")]
        public async Task<ActionResult<DeviceDto>> GetById(int deviceId)
        {            
            var device = await _context.Devices.FindAsync(deviceId);

            if (device == null)
            {
                return NotFound();
            }

            var deviceDto = _mapper.Map<DeviceDto>(device);

            return Ok(deviceDto);
        }

        // /api/device?DeviceName=Device%201  lub  /api/device/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAllOrByName ([FromQuery] string? deviceName)
        {
            var query = _context.Devices.AsQueryable();

            if (!string.IsNullOrEmpty(deviceName))
            {
                query = query.Where(s => s.DeviceName != null && s.DeviceName.Contains(deviceName));
            }

            var devices = await query.ToListAsync();

            if (devices == null || devices.Count == 0)
            {
                return NotFound();
            }

            var devicesDto = _mapper.Map<List<DeviceDto>>(devices); 

            return Ok(devicesDto);
        }

        // 

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeviceDto deviceDto)
        {
            var device = _mapper.Map<Device>(deviceDto);

            _context.Devices.Add(device);

            await _context.SaveChangesAsync();

            // Zwrócenie kodu 201 Created wraz z nagłówkiem Location
            // nameof(GetById) wskazuje na metodę, która pozwala pobrać ten konkretny zasób
            return CreatedAtAction(nameof(GetById), new { deviceId = device.DeviceId }, deviceDto);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPut("{deviceId}")]
        public async Task<IActionResult> Update(int deviceId, [FromBody] UpdateDeviceDto updatedDeviceDto)
        {
            var existingDevice = await _context.Devices.FindAsync(deviceId);

            if (existingDevice == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedDeviceDto, existingDevice);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{deviceId}")]
        public async Task<IActionResult> Delete(int deviceId)
        {
            var device = await _context.Devices.FindAsync(deviceId);

            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
