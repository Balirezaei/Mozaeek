using System.IO;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Enum;

namespace MozaeekCore.RestAPI.Utility
{
    public static class FileExtention
    {
        public static TechnicianAttachmentDto GetAttachmentDto(this IFormFile file, AttachmentType type)
        {
            var extension = Path.GetExtension(file.FileName);
            var attachment = new TechnicianAttachmentDto();
            attachment.FileName = file.FileName;
            long lengh = file.Length;
            attachment.Source = new byte[lengh];
            file.OpenReadStream().Read(attachment.Source, 0, (int)lengh);
            attachment.FileExtention = extension;
            attachment.AttachmentType = type;
            return attachment;
        }
    }
}