using LAOL.Marketplase.AuthAdminRepository;
using LAOL.Marketplase.AuthAdminRepository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAOL.Marketplase.WEB.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : ControllerBase
    {

        private readonly IFileRepository _fileRep;
        private readonly IConfiguration _configuration;
        readonly IBufferedFileUploadService _bufferedFileUploadService;

        public AdminController(IConfiguration configuration, IFileRepository fileRep, IBufferedFileUploadService bufferedFileUploadService)
        {
            _configuration = configuration;
            _fileRep = fileRep;
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        [HttpPost("createresolution")]
        public CustomMessage CreateResolution(FileResolutions request)
        {

            var messageResult = _fileRep.CreateResolution(request);

            return messageResult;
        }
        [HttpPost("updateresolution")]
        public CustomMessage UpdateResolution(FileResolutions request)
        {

            var messageResult = _fileRep.UpdateResolution(request);

            return messageResult;
        }

        [HttpGet("selectresolutions")]
        public List<FileResolutions> SelectResolutions(int param)
        {

            var resolutions = _fileRep.SelectResolutions(param);

            return resolutions;
        }

        [HttpGet("selectresolution")]
        public FileResolutions SelectResolution(int param)
        {

            var resolution = _fileRep.SelectResolution(param);

            return resolution;
        }

        [HttpGet("deleteresolution")]
        public CustomMessage DeleteResolution(int param)
        {

            var resolution = _fileRep.DeleteResolution(param);

            return resolution;
        }

      
        [HttpPost("uploadfile")]
        public CustomMessage UploadFile(FileModel file)
        {
            var msg = new CustomMessage();
            try
            {
                if (_bufferedFileUploadService.UploadFile(file.file, file.VendorId))
                {
                    msg.Message = "File uplpaded";
                    msg.Code = 200;
                }
                else
                {
                    msg.Message = "File not uplpaded";
                    msg.Code = 500;
                }
            }
            catch (Exception ex)
            {

                msg.Message = ex.Message;
                msg.Code = 500;

            }
            return msg;
        }
       
        [HttpPost("downloadfile")]
        public FileContentResult DownloadFile(DownloadFile file)
        {
            var result = _bufferedFileUploadService.DownloadFile(file.FileName);
            return result;
        }
    }
}
