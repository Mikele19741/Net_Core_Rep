using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository.Connectors
{
    public class Connectors
    {
     

        public static string CreateConnection()
        {
            return $"User ID=user;Password=password;Host=localhost;Port=5432;Database=MarketPlase;Pooling=false;Timeout=300;CommandTimeout=300";

        }

     
    }
}
