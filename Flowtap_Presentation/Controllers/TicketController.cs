using Flowtap_Application.Features.ServiceTickets.Commands.AddPartToTicket;
using Flowtap_Application.Features.ServiceTickets.Commands.AddTicketItem;
using Flowtap_Application.Features.ServiceTickets.Commands.RemoveTicketItem;
using Flowtap_Application.Features.ServiceTickets.Commands.AssignTicket;
using Flowtap_Application.Features.ServiceTickets.Commands.CollectTicket;
using Flowtap_Application.Features.ServiceTickets.Commands.CreateTicket;
using Flowtap_Application.Features.ServiceTickets.Commands.RecordTicketAdvance;
using Flowtap_Application.Features.ServiceTickets.Commands.UpdateTicket;
using Flowtap_Application.Features.ServiceTickets.Commands.UpdateTicketStatus;
using Flowtap_Application.Features.ServiceTickets.Queries.GetTicket;
using Flowtap_Application.Features.ServiceTickets.Queries.GetTicketParts;
using Flowtap_Application.Features.ServiceTickets.Queries.GetTickets;
using Flowtap_Domain.BoundedContexts.Core.Organization.Enums;
using Flowtap_Presentation.Authorization;
using Flowtap_Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flowtap_Presentation.Controllers;

[RequirePermission("ServiceTickets")]
[Route("api/v1/tickets")]
[RequiresIndustry(IndustryType.RepairShop)]
public class TicketController(ISender sender) : ApiController(sender)
{
    // POST /api/v1/tickets
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command, CancellationToken ct)
        => Created(await Sender.Send(command, ct));

    // GET /api/v1/tickets
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetTicketsQuery query, CancellationToken ct)
        => Ok(await Sender.Send(query, ct));

    // GET /api/v1/tickets/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await Sender.Send(new GetTicketQuery(CurrentTenantId, id), ct));

    // PUT /api/v1/tickets/{id}  — full field update
    public sealed class UpdateTicketRequest
    {
        public Guid? ClientId { get; set; }
        public Guid? ExecutorEmployeeId { get; set; }
        public Guid? ManagerEmployeeId { get; set; }
        public string? Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public string? DeviceType { get; set; }
        public string? DeviceBrand { get; set; }
        public string? DeviceModel { get; set; }
        public string? DeviceSerial { get; set; }
        public string? DeviceModification { get; set; }
        public string? Appearance { get; set; }
        public string? Password { get; set; }
        public string? Equipment { get; set; }
        public string? Reason { get; set; }
        public string? MastersNotes { get; set; }
        public string? PreRepairChecklist { get; set; }
        public string? AccessoryList { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? Prepayment { get; set; }
        public decimal? TotalCost { get; set; }
        public bool? IsPaid { get; set; }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketRequest req, CancellationToken ct)
    {
        var command = new UpdateTicketCommand(
            id, CurrentTenantId,
            req.ClientId,
            req.ExecutorEmployeeId, req.ManagerEmployeeId,
            req.Priority, req.Deadline,
            req.DeviceType, req.DeviceBrand, req.DeviceModel, req.DeviceSerial,
            req.DeviceModification, req.Appearance, req.Password, req.Equipment,
            req.Reason, req.MastersNotes, req.PreRepairChecklist, req.AccessoryList,
            req.EstimatedCost, req.Prepayment, req.TotalCost, req.IsPaid
        );
        return FromResult(await Sender.Send(command, ct));
    }

    // PATCH /api/v1/tickets/{id}/status
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTicketStatusCommand command, CancellationToken ct)
    {
        command = command with { TicketId = id, CompanyId = CurrentTenantId };
        return FromResult(await Sender.Send(command, ct));
    }

    // POST /api/v1/tickets/{id}/items  — add a service / part / product line item
    public sealed class AddItemRequest
    {
        public Guid ItemReferenceId { get; set; }   // serviceId or productId
        public string Name          { get; set; } = string.Empty;
        public string Type          { get; set; } = "Service"; // Service | Part | Product
        public decimal Quantity     { get; set; } = 1;
        public decimal Price        { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxPercent   { get; set; }
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddItemRequest req, CancellationToken ct)
    {
        var command = new AddTicketItemCommand(
            CompanyId:      CurrentTenantId,
            TicketId:       id,
            ItemReferenceId: req.ItemReferenceId,
            Name:           req.Name,
            Type:           req.Type,
            Quantity:       req.Quantity,
            Price:          req.Price,
            DiscountAmount: req.DiscountAmount,
            TaxPercent:     req.TaxPercent
        );
        return Created(await Sender.Send(command, ct));
    }

    // DELETE /api/v1/tickets/{id}/items/{itemId}
    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    public async Task<IActionResult> RemoveItem(Guid id, Guid itemId, CancellationToken ct)
        => FromResult(await Sender.Send(new RemoveTicketItemCommand(CurrentTenantId, id, itemId), ct));

    // POST /api/v1/tickets/{ticketId}/parts
    [HttpPost("{ticketId}/parts")]
    public async Task<IActionResult> AddPart(Guid ticketId, [FromBody] AddPartToTicketCommand command, CancellationToken ct)
        => Created(await Sender.Send(command with { ServiceTicketId = ticketId }, ct));

    // GET /api/v1/tickets/{ticketId}/parts
    [HttpGet("{ticketId}/parts")]
    public async Task<IActionResult> GetParts(Guid ticketId, [FromQuery] Guid companyId, CancellationToken ct)
        => Ok(await Sender.Send(new GetTicketPartsQuery(companyId, ticketId), ct));

    // PATCH /api/v1/tickets/{id}/assign
    [HttpPatch("{id:guid}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignTicketCommand command, CancellationToken ct)
        => FromResult(await Sender.Send(command with { CompanyId = CurrentTenantId, ServiceTicketId = id }, ct));

    // POST /api/v1/tickets/{id}/advance
    // Records an advance/deposit payment against a ticket (no Sale created yet).
    // The advance is stored on ticket.Financials.Prepayment and as a Payment row (TicketId, no SaleId).
    // When the customer collects and a Sale is created with TicketId, the advance is deducted automatically.
    public sealed class RecordAdvanceRequest
    {
        public decimal Amount { get; set; }
        public string Method { get; set; } = "Cash";   // Cash | Card | UPI | NetBanking | Wallet
        public Guid? AccountId { get; set; }
        public string? ExternalReference { get; set; }
        public string? Comment { get; set; }
        public Guid? EmployeeId { get; set; }
    }

    [HttpPost("{id:guid}/advance")]
    public async Task<IActionResult> RecordAdvance(Guid id, [FromBody] RecordAdvanceRequest req, CancellationToken ct)
    {
        var command = new RecordTicketAdvanceCommand(
            CompanyId:         CurrentTenantId,
            TicketId:          id,
            Amount:            req.Amount,
            Method:            req.Method,
            AccountId:         req.AccountId,
            ExternalReference: req.ExternalReference,
            Comment:           req.Comment,
            EmployeeId:        req.EmployeeId
        );
        return Created(await Sender.Send(command, ct));
    }

    // POST /api/v1/tickets/{id}/collect
    // Records the final payment (if any remaining balance) and converts the ticket to a Sale.
    // Marks the ticket as Done. Returns the new SaleId.
    public sealed class CollectTicketRequest
    {
        public decimal? FinalPaymentAmount { get; set; }
        public string?  FinalPaymentMethod { get; set; } = "Cash";
        public Guid?    AccountId          { get; set; }
        public string?  ExternalReference  { get; set; }
        public string?  Comment            { get; set; }
    }

    [HttpPost("{id:guid}/collect")]
    public async Task<IActionResult> Collect(Guid id, [FromBody] CollectTicketRequest req, CancellationToken ct)
    {
        var command = new CollectTicketCommand(
            CompanyId:           CurrentTenantId,
            TicketId:            id,
            FinalPaymentAmount:  req.FinalPaymentAmount,
            FinalPaymentMethod:  req.FinalPaymentMethod,
            AccountId:           req.AccountId,
            ExternalReference:   req.ExternalReference,
            Comment:             req.Comment
        );
        return FromResult(await Sender.Send(command, ct));
    }

    [HttpPost("{id:guid}/timer/toggle")]
    public async Task<IActionResult> ToggleTimer(Guid id, CancellationToken ct)
    {
        if (CurrentUserId == Guid.Empty)
            return Unauthorized();
            
        return FromResult(await Sender.Send(new Flowtap_Application.Features.ServiceTickets.Commands.ToggleTicketTimer.ToggleTicketTimerCommand(CurrentTenantId, CurrentUserId, id), ct));
    }
}
