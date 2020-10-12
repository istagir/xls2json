using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace excel2json.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SerielizerController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public SerielizerController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        public IActionResult Post(IFormFile excelfile)
        {
            var filemanager= new FileManager(_hostingEnvironment);
            string filepath = filemanager.Upload(excelfile);     // save file
            filemanager.Upload(excelfile);
            var serializer = new Serielizer(filepath);
            var stream = serializer.Convert();

            filemanager.Delete();

            return File(stream, MediaTypeNames.Application.Json, excelfile.FileName + ".json");
        }
    }
}
