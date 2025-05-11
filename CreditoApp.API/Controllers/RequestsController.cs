using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Domain.Models.Requests.CreditApp;
using CreditoApp.Domain.Models.Responses.CreditApp;
using CreditoApp.Domain.Models.Responses.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditoApp.API.Controllers
{
    [Route("api/requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly ICreditRequestService _creditRequestService;
        private readonly ICreditReviewService _creditReviewService;

        public RequestsController(ICreditRequestService creditRequestService, ICreditReviewService creditReviewService)
        {
            _creditRequestService = creditRequestService;
            _creditReviewService = creditReviewService;
        }

        [HttpGet()]
        [Authorize(Roles = "Analyst")]
        public async Task<ActionResult<List<CreditRequestResponse>>> GetCreditRequests()
        {
            var response = await _creditReviewService.GetCreditRequests();
            return Ok(new SuccessResponse<List<CreditRequestResponse>>(200, "Credit requests retrieved successfully", response));
        }

        [HttpGet("{requestId}")]
        public async Task<ActionResult<CreditRequestResponse>> GetCreditRequestById(int requestId)
        {
            var response = await _creditReviewService.GetCreditRequestById(requestId);
            return Ok(new SuccessResponse<CreditRequestResponse>(200, "Credit request retrieved successfully", response));
        }

        [HttpPut("{requestId}/status")]
        [Authorize(Roles = "Analyst")]
        public async Task<ActionResult<CreditRequestResponse>> UpdateCreditRequestStatus(int requestId, [FromBody] string status)
        {
            var response = await _creditReviewService.UpdateCreditRequestStatus(requestId, status);
            return Ok(new SuccessResponse<CreditRequestResponse>(200, "Credit request status updated successfully", response));
        }

        [HttpPost()]
        public async Task<ActionResult<CreditRequestResponse>> CreateCreditRequest([FromBody] CreateCreditRequest request)
        {
            var response = await _creditRequestService.CreateCreditRequest(request);
            return Ok(new SuccessResponse<CreditRequestResponse>(201, "Credit request created successfully", response));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<CreditRequestResponse>>> GetCreditRequestsByUserId(int userId)
        {
            var response = await _creditRequestService.GetCreditRequestsByUserId(userId);
            return Ok(new SuccessResponse<List<CreditRequestResponse>>(200, "Credit requests retrieved successfully", response));
        }


    }
}