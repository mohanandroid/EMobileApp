using System;
using System.Threading.Tasks;

namespace eCups.Services
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
