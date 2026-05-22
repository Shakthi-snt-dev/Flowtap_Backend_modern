using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.UpdateService;

public record UpdateServiceCommand(
    Guid Id, Guid CompanyId, string? Name, string? Description,
    decimal? BasePrice, string? EstimatedDuration, bool? IsActive) : IRequest<Result<bool>>;
