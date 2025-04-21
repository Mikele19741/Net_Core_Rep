using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository
{
    public interface IBufferedFileUploadService
    {
        bool UploadFile(IFormFile file, int vendorId);

        FileContentResult? DownloadFile(string path);
    }
}
