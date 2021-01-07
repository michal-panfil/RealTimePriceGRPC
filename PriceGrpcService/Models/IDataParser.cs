using System.Collections.Generic;
using System.Threading.Tasks;

namespace PriceGrpcService.Models
{
    public interface IDataParser
    {
        Task<List<InstrumentPrice>> ParseData(string rawData);
    }
}
