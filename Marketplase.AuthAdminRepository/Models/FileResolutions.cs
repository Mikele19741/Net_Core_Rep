using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository.Models
{
    public class FileResolutions
    {
        public int Id { get; set; }
        public string Resolution { get; set; }
        public int Vendorid { get; set; }
        public int UserId { get; set; }

        public string VendorName { get; set; }
        public string UserName { get; set; }
    }

    public class VendorFiles
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int Vendorid { get; set; }
    }
}
