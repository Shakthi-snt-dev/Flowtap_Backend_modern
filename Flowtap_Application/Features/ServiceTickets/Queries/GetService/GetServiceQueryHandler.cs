using AutoMapper;
using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetService;

public class GetServiceQueryHandler(IApplicationDbContext db, IMapper mapper)
    : IRequestHandler<GetServiceQuery, Result<ServiceDto>>
{
    public async Task<Result<ServiceDto>> Handle(GetServiceQuery request, CancellationToken ct)
    {
        var service = await db.Services
            .FirstOrDefaultAsync(s => s.CompanyId == request.CompanyId && s.Id == request.ServiceId, ct)
            ?? throw new NotFoundException(nameof(Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities.Service), request.ServiceId);

        return Result<ServiceDto>.Success(mapper.Map<ServiceDto>(service));
    }
}
