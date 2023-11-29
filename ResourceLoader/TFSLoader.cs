using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ResourceLoader
{
    public class TFSLoader
    {
        //encode your personal access token                   
        public string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", "mcf7n6aids2rlvjsforkcrsfsmy5q7j5lir5br6nniehc4uasy5q")));

        public async Task<string> FetchFile()
        {
            const String URL = "https://dev.azure.com/TFSLoader/TFS/_apis/git/repositories/84ead55b-8c0a-4abe-b209-5904e27cafbc/items?path=/sqlFiles/test.sql&api-version=6.1-preview.1"; //item URL
            string result = "No file found!";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            HttpResponseMessage response = client.GetAsync("_apis/projects?stateFilter=All&api-version=1.0").Result;
            if(response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
