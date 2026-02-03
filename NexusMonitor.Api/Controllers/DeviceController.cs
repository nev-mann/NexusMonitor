using Microsoft.AspNetCore.Mvc;

namespace NexusMonitor.Api.Controllers
{

    [ApiController]

    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private static readonly List<Device> _devices = new()
        {
            new Device { DeviceId = 1, DeviceName = "Device 1", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) },
            new Device { DeviceId = 2, DeviceName = "Device 2", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) },
            new Device { DeviceId = 3, DeviceName = "Device 3", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) }
        };

        // /api/device/1
        [HttpGet("{deviceId}")]
        public ActionResult<Device> GetById(int deviceId)
        {
            var device = _devices.FirstOrDefault(s => s.DeviceId == deviceId);
            return device == null ? NotFound() : Ok(device);
        }

        // /api/device?DeviceName=Device%201  lub  /api/device/
        [HttpGet]
        public ActionResult GetAllOrByName ([FromQuery] string? deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                return Ok(_devices);
            }

            var device = _devices.FirstOrDefault(s => s.DeviceName == deviceName);
            return device == null ? NotFound() : Ok(device);
        }

        // 

        [HttpPost]
        public IActionResult Create([FromBody] CreateDeviceDto deviceDto)
        {
            var device = new Device
            {
                DeviceId = _devices.Any() ? _devices.Max(d => d.DeviceId) + 1 : 1,
                DeviceName = deviceDto.DeviceName,
                SecretKey = RandomString(12)
            };

            _devices.Add(device);

            // Zwrócenie kodu 201 Created wraz z nagłówkiem Location
            // nameof(GetById) wskazuje na metodę, która pozwala pobrać ten konkretny zasób
            return CreatedAtAction(nameof(GetById), new { deviceId = device.DeviceId }, device);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //

        [HttpPut("{deviceId}")]
        public IActionResult Update(int deviceId, [FromBody] CreateDeviceDto updatedDeviceDto)
        {
            var existingDevice = _devices.FirstOrDefault(s => s.DeviceId == deviceId);

            if (existingDevice == null)
            {
                return NotFound();
            }

            existingDevice.DeviceName = updatedDeviceDto.DeviceName;

            return NoContent();
        }
        //

        [HttpDelete("{deviceId}")]
        public IActionResult Delete(int deviceId)
        {
            var device = _devices.FirstOrDefault(s => s.DeviceId == deviceId);

            if (device == null)
            {
                return NotFound();
            }

            _devices.Remove(device);

            return NoContent();
        }
    }
}
