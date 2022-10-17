using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karmizban.Support.ApplicationService.Contract;
using Karmizban.Support.Common;
using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;

namespace Karmizban.Support.ApplicationService
{
    public interface IUserReceivedMessageService
    {
        Task<List<UserReceivedMessageDto>> GetAll(GetUserReceivedMessageContract contract);
        Task<UserRequestSupportAnswerDetail> GetAnswerDetail(GetUserRequestSupportAnswerContract contract);
    }
    public class UserReceivedMessageService : IUserReceivedMessageService
    {
        private readonly IUserRequestSupportAnswerRepository _userRequestSupportAnswerRepository;

        public UserReceivedMessageService(IUserRequestSupportAnswerRepository userRequestSupportAnswerRepository)
        {
            _userRequestSupportAnswerRepository = userRequestSupportAnswerRepository;
        }

        public async Task<List<UserReceivedMessageDto>> GetAll(GetUserReceivedMessageContract contract)
        {
            var list = await _userRequestSupportAnswerRepository.GetAll()
                .Skip((contract.PageNumber - 1) * contract.PageSize)
                .Take(contract.PageSize).ToListAsync();

            return list.Select(m => new UserReceivedMessageDto
            {
                Title = m.Title,
                CreateDate = m.CreateDate.GetTimeFromNow(),
                Id = m.Id
            }).ToList();
        }

        public async Task<UserRequestSupportAnswerDetail> GetAnswerDetail(GetUserRequestSupportAnswerContract contract)
        {
            var answer = await _userRequestSupportAnswerRepository.GetAll()
                .Include(m => m.UserRequestSupport)
                .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == contract.AnswerId);

            if (answer.UserRequestSupport.User.UserId != contract.UserId)
            {
                throw new Exception("اطلاعات یافت نشد");
            }
            return new UserRequestSupportAnswerDetail()
            {
                Title = answer.Title,
                CreateDate = answer.CreateDate.GetTimeFromNow(),
                Description = answer.AnswerDescription,
                Id = answer.Id
            };
        }
    }
}