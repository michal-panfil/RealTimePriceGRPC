using System.Threading.Tasks;

namespace PriceGrpcService.Models
{
    public interface IDataDownloader
    {
        Task<string> DownloadData(string source);
    }
}
