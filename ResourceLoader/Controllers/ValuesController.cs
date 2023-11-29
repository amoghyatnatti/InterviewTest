using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceLoader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceLoader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController(FileLoaderService fileService)
        {
            FileService = fileService;
        }

        public FileLoaderService FileService { get; }

        [HttpGet]
        public string Get()
        {
            return FileService.GetFile();
        }
    }
}
