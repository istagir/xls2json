using System;
using System.IO;
using System.Threading.Tasks;
using ExcellParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.ExcellParser;


namespace WebAPIApp.Controllers
{
    
    [ApiController]
    [Route("/app/[controller]")]
    public class SerializerController : Controller
    {
        private String path = "../";
        private String name = "file.xlsx";
   
        private Parser serializer;
    

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
                    serializer=new ColumnParser();
                    string result =  serializer.parse(path+name);
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