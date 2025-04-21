using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository.Models
{
    public class Logger
    {
        public static Logger Instance = new Logger();


        public Logger() { }

        public void CreateMessage(LoggerModel message)
        {
            try
            {

                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();
                var sql = $@"INSERT INTO public.logger(
	                         messages, event_type)
	                        VALUES ( '{message.Messages}', {message.EnventType});";
                var cmd = new NpgsqlCommand(sql, objpostgraceConn);
                cmd.ExecuteNonQuery();
                objpostgraceConn.Close();

            }
            catch
            {

            }


        }
    }
}
