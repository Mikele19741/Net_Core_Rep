using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository.Models
{
    public class CustomMessage
    {
        public string Message { get;  set; }
         public int Code { get;  set; }
    }
    public enum EventTypes
    {
        Info = 0,
        Warning = 1,
        Error = 2,
    }
    public class LoggerModel
    {
        public int Id { get; set; }
        public string Messages { get; set; }

        public int EnventType { get; set; }
    }
}
