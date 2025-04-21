using LAOL.Marketplase.AuthAdminRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository
{
    public interface IFileRepository
    {
        public CustomMessage CreateResolution(FileResolutions resolutions);
        public CustomMessage UpdateResolution(FileResolutions resolutions);

        public List<FileResolutions> SelectResolutions(int parametr);
        public FileResolutions SelectResolution(int id);

        public CustomMessage DeleteResolution(int id);
    }
}
