using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases
{
    public class MarkRentalAllocationAsDispatched : IBusinessUseCase
    {
        public string PickerName { get; set; }

        public int DvdId { get; set; }

        public string FulfilmentRequestId { get; set; }
    }
}
