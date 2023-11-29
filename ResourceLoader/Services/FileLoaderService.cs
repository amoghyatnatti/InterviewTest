using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceLoader.Services
{
    public class FileLoaderService
    {
        public FileLoaderService(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            WebHostEnvironment = webHostEnvironment;
            Configuration = config;
            Loader = GetLoader();
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IConfiguration Configuration { get; }
        public TFSLoader Loader { get; }

        private string FileFolderPath
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "Test"); }
        }

        public string GetString()
        {
            return Configuration.GetValue<string>("ResourceLoader:PreferredLoader");
        }

        private TFSLoader GetLoader()
        { 
            return new TFSLoader();
        }

        public async Task<string> GetFile()
        {
            return Loader.FetchFile();
        }
    }
}
