using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Infrastructure
{
    public class Application
    {
        private ICommandHandlerRegistry _command_handler_registery;

        public Application(ICommandHandlerRegistry command_handler_registery)
        {
            _command_handler_registery = command_handler_registery;
        }

        public void action_request_to<T>(T business_case) where T : IBusinessUseCase
        {
            _command_handler_registery.find_handler_for(business_case).Invoke(business_case);
        }
    }
}
