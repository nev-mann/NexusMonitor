using Microsoft.AspNetCore.Mvc;

namespace NexusMonitor.Api.Controllers
{

    [ApiController]

    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private static readonly List<Device> _devices = new()
        {
            new Device { DeviceId = "1", DeviceName = "Device 1", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) },
            new Device { DeviceId = "2", DeviceName = "Device 2", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) },
            new Device { DeviceId = "3", DeviceName = "Device 3", DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow) }
        };

        // /api/device/
        /*
        [HttpGet]
        public ActionResult<List<Device>> GetAll()
        {
            return Ok(_devices);
        }
        */

        // /api/device/1
        [HttpGet("{id}")]
        public ActionResult<Device> GetById(int DeviceId)
        {
            return Ok(_devices[0]);
        }

        // /api/device?DeviceID=1
        [HttpGet]
        public ActionResult<Device> GetAll([FromQuery] string? DeviceId)
        {
            var device = _devices.FirstOrDefault(s => s.DeviceId == DeviceId);

            if (device == null)
            {
                wreturn NotFound();
            }

            return Ok(device);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Device updatedSong)
        {
            // 1. Szukamy piosenki do edycji
            var existingSong = _dev.FirstOrDefault(s => s.Id == id);

            // 2. Jeśli nie ma - 404 Not Found
            if (existingSong == null)
            {
                return NotFound();
            }

            // 3. Aktualizacja pól
            // (Uwaga: ID w URL musi się zgadzać z ID obiektu, lub ignorujemy ID z body)
            existingSong.Title = updatedSong.Title;
            existingSong.Artist = updatedSong.Artist;

            // 4. Zwracamy 204 No Content
            // Oznacza to: "Zrobione, nie muszę Ci odsyłać obiektu, który właśnie wysłałeś"
            return NoContent();
        }


        // 
        [HttpPost]
        public IActionResult Create([FromBody] Device device)
        {
            string newId = RandomString(12);
            device.DeviceId = newId;

            _devices.Add(device);

            // 3. Zwrócenie kodu 201 Created wraz z nagłówkiem Location
            // nameof(GetById) wskazuje na metodę, która pozwala pobrać ten konkretny zasób
            return CreatedAtAction(nameof(GetById), new { id = device.DeviceId }, device);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        } 
    }
}
