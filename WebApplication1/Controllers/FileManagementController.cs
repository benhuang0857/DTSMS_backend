using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using DTSMS.Models;
using Microsoft.EntityFrameworkCore;

namespace DTSMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagementController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public FileManagementController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<FileManagementController>
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] ApplicationDbContext dbContext)
        {
            var files = await dbContext.FileModel
                           .Select(file => new FileModel
                           {
                               FileName = file.FileName,
                               Size = file.Size,
                               TimeStamp = DateTime.Now
                           })
                           .ToListAsync();

            return Ok(files);
        }

        // GET api/<FileManagementController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] ApplicationDbContext dbContext)
        {
            var file = await dbContext.FileModel
                            .FirstOrDefaultAsync(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        // POST api/<FileManagementController>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Request.Headers["FileName"].ToString());
            var fullPath = Path.Combine(filePath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await Request.Body.CopyToAsync(stream);
            }

            var fileRecord = new FileModel
            {
                FileName = fileName,
                MemberId = 1,
                Size = new FileInfo(fullPath).Length,
                Checksum = 0,
                Stage = "Uploaded",
                Status = "Success",
                TimeStamp = DateTime.UtcNow
            };

            _dbContext.FileModel.Add(fileRecord);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "File uploaded successfully.", FileId = fileRecord.Id });
        }

        // PUT api/<FileManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
