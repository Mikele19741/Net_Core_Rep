using LAOL.Marketplase.AuthAdminRepository.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository
{
    public class FileRepository : IFileRepository
    {
        public FileResolutions SelectResolution(int id)
        {
            var item = new FileResolutions();
            try
            {
                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();

                var sql = $@"SELECT vr.id as id, vr.vendor_id as vendor, vr.vendor_resolution as vendor_resolution , 
                            c.name_of_the_vendor as name_of_the_vendor, 
                            u.email as email,  vr.user_id as user_id
	                            FROM public.vendor_resolutions as vr
	                            JOIN public.users u ON u.user_id = vr.user_id
	                            JOIN public.customers c ON vr.vendor_id = c.id;
	                            WHERE vr.id==" + id;

                var cmd = new NpgsqlCommand(sql, objpostgraceConn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    item.Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"].ToString());
                    item.UserId = reader["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["user_id"]);
                    item.Resolution = reader["vendor_resolution"] == DBNull.Value ? "" : reader["vendor_resolution"].ToString();
                    item.VendorName = reader["name_of_the_vendor"] == DBNull.Value ? "" : reader["name_of_the_vendor"].ToString();
                    item.UserName = reader["email"] == DBNull.Value ? "" : reader["email"].ToString();
                    item.Vendorid = reader["vendor"] == DBNull.Value ? 0 : Convert.ToInt32(reader["vendor"]);
                    item.UserId = reader["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["user_id"]);





                }
                objpostgraceConn.Close();



                return item;



            }
            catch (Exception ex)
            {
                Logger.Instance.CreateMessage(new LoggerModel() { Messages = $"{ex.Message}", EnventType = (int)EventTypes.Error });
                item = new FileResolutions();

            }
            return item;
        }

        public List<FileResolutions> SelectResolutions(int parametr)
        {
            var items= new List<FileResolutions>();
            var item = new FileResolutions();
            try
            {
                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();

                var sql = $@"SELECT vr.id as id, vr.vendor_id as vendor, vr.vendor_resolution as vendor_resolution , 
                            c.name_of_the_vendor as name_of_the_vendor, 
                            u.email as email,  vr.user_id as user_id
	                            FROM public.vendor_resolutions as vr
	                            JOIN public.users u ON u.user_id = vr.user_id
	                            JOIN public.customers c ON vr.vendor_id = c.id;
	                            ";

                if(parametr>0)
                {
                    sql = sql + " WHERE  vr.vendor_id=" + parametr;
                }

                var cmd = new NpgsqlCommand(sql, objpostgraceConn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    item.Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"].ToString());
                    item.UserId = reader["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["user_id"]);
                    item.Resolution = reader["vendor_resolution"] == DBNull.Value ? "" : reader["vendor_resolution"].ToString();
                    item.VendorName = reader["name_of_the_vendor"] == DBNull.Value ? "" : reader["name_of_the_vendor"].ToString();
                    item.UserName = reader["email"] == DBNull.Value ? "" : reader["email"].ToString();
                    item.Vendorid = reader["vendor"] == DBNull.Value ? 0 : Convert.ToInt32(reader["vendor"]);
                    item.UserId = reader["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["user_id"]);


                    items.Add(item);


                }
                objpostgraceConn.Close();



             



            }
            catch (Exception ex)
            {
                Logger.Instance.CreateMessage(new LoggerModel() { Messages = $"{ex.Message}", EnventType = (int)EventTypes.Error });
                item = new FileResolutions();

            }
            return items;
        }

        public CustomMessage CreateResolution(FileResolutions resolutions)
        {
            try
            {

                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();
                var sql = $@"INSERT INTO public.vendor_resolutions(
	vendor_id, vendor_resolution, user_id)
	VALUES ({resolutions.Vendorid}, '{resolutions.Resolution}', {resolutions.UserId})";
                var cmd = new NpgsqlCommand(sql, objpostgraceConn);
                Object res = cmd.ExecuteScalar();
                objpostgraceConn.Close();
                var x = Convert.ToInt32(res);
                if (x > 0)
                {

                }


                return new CustomMessage()
                {
                    Message = "Resolution_Created",
                    Code = 200
                };


            }
            catch (Exception ex)
            {
                Logger.Instance.CreateMessage(new LoggerModel() { Messages = $"{ex.Message}", EnventType = (int)EventTypes.Error });
                return new CustomMessage()
                {
                    Message = ex.Message

                };

            }
        }

        public CustomMessage DeleteResolution(int id)
        {
            try
            {

                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();
                var sql = $"DELETE FROM public.vendor_resolutions WHERE id={id};";
                var cmd = new NpgsqlCommand(sql, objpostgraceConn);
                Object res = cmd.ExecuteScalar();
                objpostgraceConn.Close();
                var x = Convert.ToInt32(res);
                if (x > 0)
                {


                    return new CustomMessage()
                    {

                        Message = "Resolution was deleted",
                        Code=200

                    };
                }


                else
                {
                    return new CustomMessage()
                    {
                        Message = "EXCEPTION",
                        Code = 500
                    };
                }

            }
            catch (Exception ex)
            {
                return new CustomMessage()
                {
                    Message = ex.Message,
                    Code = 500

                };

            }
        }

        public CustomMessage UpdateResolution(FileResolutions resolutions)
        {
            NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
            objpostgraceConn.Open();
            var sql = $@"UPDATE public.vendor_resolutions
	                    SET  vendor_id={resolutions.Vendorid}, vendor_resolution={resolutions.Resolution}, user_id={resolutions.UserId}
	                    WHERE id={resolutions.Id};";

            var cmd = new NpgsqlCommand(sql, objpostgraceConn);
            Object res = cmd.ExecuteScalar();
            objpostgraceConn.Close();
            var x = Convert.ToInt32(res);
            return new CustomMessage()
            {
                Message = "Resolution_Updated"
            };
        }
    }
}
