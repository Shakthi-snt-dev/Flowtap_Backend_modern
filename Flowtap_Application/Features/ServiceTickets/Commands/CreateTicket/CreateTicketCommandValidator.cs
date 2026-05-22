using FluentValidation;

namespace Flowtap_Application.Features.ServiceTickets.Commands.CreateTicket;

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Priority).NotEmpty();
        RuleFor(x => x.EstimatedCost).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Prepayment).GreaterThanOrEqualTo(0);
    }
}
