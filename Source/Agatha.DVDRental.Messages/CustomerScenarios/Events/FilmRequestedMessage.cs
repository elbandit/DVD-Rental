using System;
using NServiceBus;

namespace Agatha.DVDRental.Messages.CustomerScenarios.Events
{
    public class FilmRequestedMessage : IEvent
    {
        public Guid UniqueMessageId { get; set; }

        public int FilmId { get; set; }
        public int MemberId { get; set; }
    }   
}
