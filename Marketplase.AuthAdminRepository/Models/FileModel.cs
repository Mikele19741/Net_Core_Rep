using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository.Models
{
    public class FileModel
    {
        public IFormFile file {  get; set; }
        public int VendorId { get; set; }   
    }
}
