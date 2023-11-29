using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceLoader
{
    public class FileLoader : ILoader
    {

        public FileLoader(string filepath)
        {
            Filepath = filepath;
        }

        public string Filepath { get; }
        public string GetFile()
        {
            string sqlText = "No file found!"; 
            if(File.Exists(Filepath))
            {
                sqlText = File.ReadAllText(Filepath);
            }
            return sqlText;
        }
    }
}
