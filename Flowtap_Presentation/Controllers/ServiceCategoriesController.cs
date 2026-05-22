using Flowtap_Application.Features.ServiceTickets.Commands.CreateServiceCategory;
using Flowtap_Application.Features.ServiceTickets.Queries.GetServiceCategories;
using Flowtap_Domain.BoundedContexts.Core.Organization.Enums;
using Flowtap_Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flowtap_Presentation.Controllers;

[Route("api/v1/service-categories")]
[RequiresIndustry(IndustryType.RepairShop)]
public class ServiceCategoriesController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCategoryCommand command, CancellationToken ct)
        => Created(await Sender.Send(command, ct));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetServiceCategoriesQuery query, CancellationToken ct)
        => Ok(await Sender.Send(query, ct));
}
