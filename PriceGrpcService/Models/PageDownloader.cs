using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PriceGrpcService.Models
{
    public class PageDownloader : IDataDownloader
    {
        public  async Task<string> DownloadData(string pageUrl)
        {
            var httpClient = new HttpClient();

            //need it to trick website
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            try
            {
                using (var response = await httpClient.GetAsync(pageUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
