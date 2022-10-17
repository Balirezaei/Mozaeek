using System;

namespace Karmizban.Support.ApplicationService.Contract
{
    public class CreateUserSuggestedSupportCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
