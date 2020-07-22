using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using waRemoteFileSystem.Core;
using waRemoteFileSystem.Models;
using waRemoteFileSystem.Utils;

namespace waRemoteFileSystem.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RfsController : ControllerBase
    {
        private readonly IFileSystem fs;
        private readonly ILogger<RfsController> log;

        public RfsController(ILogger<RfsController> _log, IFileSystem _fs)
        {
            fs = _fs;
            log = _log;
        }

        [HttpPost("create_directory")]
        public IActionResult CreateDirectory(SmRequest param)
        {
            var res = fs.CreateDirectory(param.Value);
            return Ok(res);
        }

      
        [HttpPost("delete_file")]
        public IActionResult DeleteFile(SmRequest param)
        {
            var res = fs.DeleteFile(param.Value);
            return Ok(res);
        }

        [HttpPost("directory_exists")]
        public IActionResult DirectoryExists(SmRequest param)
        {
            var res = fs.DirectoryExists(param.Value);
            return Ok(res);
        }

        [HttpPost("file_exists")]
        public IActionResult FileExists(SmRequest param)
        {
            var res = fs.DirectoryExists(param.Value);
            return Ok(res);
        }


        [HttpPost("get_list")]
        public IActionResult GetList(SmRequest param)
        {
            var res = fs.GetList(param.Value);
            if (res.IsNotFound) return NotFound("Directory not found");

            return Ok(res.list);
        }


        [HttpGet("getfile/{fpath}")]
        public ActionResult GetFile(string fpath)
        {
            var (data, extension, res) = fs.GetFile(fpath);

            if (res)
                return File(data, MimeTypeMap.GetMimeType(extension));

            return NotFound();
        }
         

        [HttpPost("create_file")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateFile([FromForm(Name = "file")] IFormFile userfile)
        {
            if ( userfile.FileName == "" ) BadRequest();
            //if (userfile.Length > 200000) BadRequest("Файл хажми катта 200KB дан...");

            var r = await fs.CreateFileAsync(userfile);
            return Ok(r);
        }
    }
}
