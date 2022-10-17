using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    //public class AddingToTechnicianPointCommandHandler : IBaseAsyncCommandHandler<AddingToTechnicianPointCommand, RegisterTechnicianCommandResult>
    //{
    //    private readonly ITechnicianRepository _repository;
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMessagePublisher _publisher;

    //    public AddingToTechnicianPointCommandHandler(ITechnicianRepository repository,
    //        IUnitOfWork unitOfWork,
    //        IMessagePublisher publisher)
    //    {
    //        this._repository = repository;
    //        this._unitOfWork = unitOfWork;
    //        this._publisher = publisher;
    //    }

    //    public async Task<RegisterTechnicianCommandResult> HandleAsync(AddingToTechnicianPointCommand cmd)
    //    {
    //        var technician = await _repository.FindWithPoint(cmd.TechnicianId);
    //        var points = cmd.PointId.Select(m => new TechnicianPoint
    //        {
    //            TechnicianId = cmd.TechnicianId,
    //            PointId = m
    //        });

    //        technician.AppendPoints(points);
    //        await _unitOfWork.CommitAsync();
    //        await _publisher.PublishAsync(
    //            new TechnicianPointAdded(technician.Id,
    //                points.Select(m => m.PointId).ToList()));

    //        return new RegisterTechnicianCommandResult()
    //        {
    //            Id = cmd.TechnicianId
    //        };

    //    }
    //}
}