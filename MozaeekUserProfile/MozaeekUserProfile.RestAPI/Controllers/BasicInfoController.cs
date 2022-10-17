// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MozaeekUserProfile.ApplicationService.Contract;
// using MozaeekUserProfile.ApplicationService.Services.BasicInfoQuery;
// using MozaeekUserProfile.Core.Core.ResponseMessages;
//
// namespace MozaeekUserProfile.RestAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class BasicInfoController : ControllerBase
//     {
//         private readonly IBasicInfoReadService _readService;
//
//         public BasicInfoController(IBasicInfoReadService readService)
//         {
//             _readService = readService;
//         }
//         [HttpGet("GetAllParentLabel")]
//         public async Task<Result<List<LabelQueryDto>>> GetAllParentLabel()
//         {
//             return ResponseFactory.Create<List<LabelQueryDto>>(await _readService.GetAllParentLabel());
//         }
//
//         [HttpGet("GetAllChildrenLabel")]
//         public async Task<Result<List<LabelQueryDto>>> GetAllChildrenLabel(long parentId)
//         {
//             return ResponseFactory.Create<List<LabelQueryDto>>(await _readService.GetAllChildrenLabel(parentId));
//         }
//
//         [HttpGet("GetAllParentSubject")]
//
//         public async Task<Result<List<SubjectQueryDto>>> GetAllParentSubject()
//         {
//             return ResponseFactory.Create<List<SubjectQueryDto>>(await _readService.GetAllParentSubject());
//         }
//
//         [HttpGet("GetAllChildrenSubject")]
//         public async Task<Result<List<SubjectQueryDto>>> GetAllChildrenSubject(long parentId)
//         {
//             return ResponseFactory.Create<List<SubjectQueryDto>>(await _readService.GetAllChildrenSubject(parentId));
//         }
//
//         [HttpGet("GetAllParentRequestOrg")]
//         public async Task<Result<List<RequestOrgQueryDto>>> GetAllParentRequestOrg()
//         {
//             return ResponseFactory.Create<List<RequestOrgQueryDto>>(await _readService.GetAllParentRequestOrg());
//         }
//
//         [HttpGet("GetAllChildrenRequestOrg")]
//         public async Task<Result<List<RequestOrgQueryDto>>> GetAllChildrenRequestOrg(long parentId)
//         {
//             return ResponseFactory.Create<List<RequestOrgQueryDto>>(await _readService.GetAllChildrenRequestOrg(parentId));
//         }
//
//         // [HttpGet("GetAllRequestTarget")]
//         // public async Task<Result<List<RequestTargetQueryDto>>> GetAllRequestTarget()
//         // {
//         //     return ResponseFactory.Create<List<RequestTargetQueryDto>>(await _readService.GetAll RequestTarget());
//         // }
//
//         [HttpGet("GetAllCity")]
//         public async Task<Result<List<PointQueryDto>>> GetAllCity()
//         {
//             return ResponseFactory.Create<List<PointQueryDto>>(await _readService.GetAllCity());
//         }
//         
//
//     }
// }
