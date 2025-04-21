using LAOL.Marketplase.AuthAdminRepository.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        public FileContentResult DownloadFile(string path)
        {

            var file = GetFromDB(path);
                string base64data = string.Empty;
                string fileName = DateTime.Now.ToString() + "_MyData.csv";

                base64data = file;

                byte[] bytes = Convert.FromBase64String(base64data);

                var response = new FileContentResult(bytes, "text/csv");
                response.FileDownloadName = fileName;

                return response;
        }

      

        private string GetFromDB(string path)
        {
            var item = string.Empty;
            try
            {
                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();

                var sql = $@"SELECT id, file, file_name, file_extension, vendor_id
	FROM public.vendor_files where file_name" + path;

                var cmd = new NpgsqlCommand(sql, objpostgraceConn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {


                    item = reader["file"] == DBNull.Value ? "" : reader["file"].ToString();
                    
                }
                objpostgraceConn.Close();



                return item;



            }
            catch (Exception ex)
            {
                Logger.Instance.CreateMessage(new LoggerModel() { Messages = $"{ex.Message}", EnventType = (int)EventTypes.Error });
               

            }
            return item;
        }
    

        public bool UploadFile(IFormFile file, int vendorId)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    string filePath = Path.Combine(path, file.FileName);
                    byte[] bytes = File.ReadAllBytes(filePath);
                    string fileBase64 = Convert.ToBase64String(bytes);
                    InsertFileInDB(file, fileBase64, vendorId);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        private void InsertFileInDB(IFormFile file, string fileBase64, int vendorId)
        {
            try
            {

                NpgsqlConnection objpostgraceConn = new NpgsqlConnection(Connectors.Connectors.CreateConnection());
                objpostgraceConn.Open();
                var sql = $@"INSERT INTO public.vendor_files(
	file, file_name, file_extension, vendor_id)
	VALUES ('{fileBase64}', '{file.FileName}', '',  {vendorId});  RETURNING id";
                var cmd = new NpgsqlCommand(sql, objpostgraceConn);
                Object res = cmd.ExecuteScalar();
                objpostgraceConn.Close();
                var x = Convert.ToInt32(res);
                if (x > 0)
                {

                }


              


            }
            catch (Exception ex)
            {
                Logger.Instance.CreateMessage(new LoggerModel() { Messages = $"{ex.Message}", EnventType = (int)EventTypes.Error });
               

            }
        }
    }
}
