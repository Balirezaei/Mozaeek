using System.IO;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Enum;

namespace MozaeekCore.RestAPI.Utility
{
    public static class FileExtension
    {
        //public static TechnicianAttachmentDto GetAttachmentDto(this IFormFile file, AttachmentType type)
        //{
        //    var extension = Path.GetExtension(file.FileName);
        //    var attachment = new TechnicianAttachmentDto();
        //    attachment.FileName = file.FileName;
        //    long lengh = file.Length;
        //    attachment.Source = new byte[lengh];
        //    file.OpenReadStream().Read(attachment.Source, 0, (int)lengh);
        //    attachment.FileExtension = extension;
        //    attachment.AttachmentType = type;
        //    return attachment;
        //}
        //public static AttachmentDto GetAttachmentDto(this IFormFile file)
        //{
        //    var extension = Path.GetExtension(file.FileName);
        //    var attachment = new AttachmentDto();
        //    attachment.FileName = file.FileName;
        //    long lengh = file.Length;
        //    attachment.Source = new byte[lengh];
        //    file.OpenReadStream().Read(attachment.Source, 0, (int)lengh);
        //    attachment.FileExtension = extension;
        //    return attachment;
        //}
    }
}