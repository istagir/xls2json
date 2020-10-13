using System;
using System.IO;
using System.Threading.Tasks;
using ExcellParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;


namespace WebAPIApp.Controllers
{
    
    [ApiController]
    [Route("/app/[controller]")]
    public class SerializerController : Controller
    {
        private String path = "../WebApplication1/SaveFile/";
        private String name = "File.xlsx";
        private Parser serializer;
        public SerializerController( Parser serializer)
        {
            this.serializer = serializer;
        }

        [HttpPost]
        public IActionResult Post(IFormFile loadfile)
        {
           
            if (loadfile!= null) {
                try
                {
                    var file = loadfile;
                    using (FileStream fs = new FileStream(path + name, FileMode.CreateNew, FileAccess.Write,
                        FileShare.Write))
                    {
                        file.CopyTo(fs);
                    }

                    string result = Task.Factory.StartNew(() =>
                    {
                        return serializer.parse(path+name);
                    }).Result;
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
                finally
                {
                    System.IO.File.Delete(path+name);
                }
            }
            return BadRequest();
        }
    }
}