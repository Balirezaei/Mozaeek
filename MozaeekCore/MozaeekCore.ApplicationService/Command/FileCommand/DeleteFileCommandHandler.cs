using Microsoft.Extensions.Options;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Common;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.FileCommand
{
    public class DeleteFileCommandHandler : IBaseAsyncCommandHandler<DeleteFileCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRepository _repository;
        private readonly RestApiAppSettings _appSettings;

        public DeleteFileCommandHandler(IUnitOfWork unitOfWork, IFileRepository repository, IOptions<RestApiAppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _appSettings = appSettings.Value;
        }

        public async Task<string> HandleAsync(DeleteFileCommand cmd)
        {
            var file =await _repository.Find(cmd.Id);
            if (File.Exists(file.Path))
                File.Delete(file.Path);
            _repository.Delete(file);
            await _unitOfWork.CommitAsync();
            return "Done";
        }
    }
}
