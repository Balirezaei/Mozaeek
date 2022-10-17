using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateFileCommandHandler : IBaseAsyncCommandHandler<CreateFileCommand, CreateFileCommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRepository _repository;
        private readonly RestApiAppSettings _appSettings;

        public CreateFileCommandHandler(IUnitOfWork unitOfWork, IFileRepository repository, IOptions<RestApiAppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _appSettings = appSettings.Value;
        }
        public async Task<CreateFileCommandResult> HandleAsync(CreateFileCommand cmd)
        {
            var name = DateTime.Now.Ticks + Path.GetExtension(cmd.File.FileName);

            var filePath = _appSettings.PhysicalFilePath + name;
            var url = _appSettings.FileNetworkUrl + name;
            var file = new MosaikFile(0, cmd.File.FileName, filePath, Path.GetExtension(cmd.File.FileName), url, cmd.Type);
            if (cmd.File.Length > 0)
            {
                using (var stream = File.Create(filePath))
                {
                    await cmd.File.CopyToAsync(stream);
                }
            }
            await _repository.Create(file);
            await _unitOfWork.CommitAsync();
            return new CreateFileCommandResult()
            {
                Id = file.Id,
                Url = url,
                Path = filePath,
                Name = name
            };
        }

    }
}
