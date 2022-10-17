using Microsoft.AspNetCore.Http;

namespace MozaeekCore.ApplicationService.Contract.File
{
    public class QuestionAttachmentDto
    {
        public IFormFile File { get; set; }
        public string Title { get; set; }
    }
}