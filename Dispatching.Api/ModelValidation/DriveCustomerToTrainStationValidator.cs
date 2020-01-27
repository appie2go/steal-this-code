using FluentValidation;
using Dispatching.Broker.Commands;
using System;

namespace Dispatching.Api.ModelValidation
{
    public class DriveCustomerToTrainStationValidator : AbstractValidator<DriveCustomerToTrainStation>
    {
        public DriveCustomerToTrainStationValidator()
        {
            RuleFor(x => x.CurrentLatitude).GreaterThan(0);
            RuleFor(x => x.CurrentLongitude).GreaterThan(0);
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty);
        }
    }
}