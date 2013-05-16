using System;
using StructureMap;

namespace Agatha.DVDRental.Infrastructure
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        public Action<TCommand> find_handler_for<TCommand>(TCommand command) where TCommand : IBusinessUseCase
        {
            var handler = ObjectFactory.TryGetInstance<IBusinessUseCaseHandler<TCommand>>();

            var transactional_handler = ObjectFactory.GetInstance<TransactionHandler>();

            Action<TCommand> method_to_handle_command = cmd => transactional_handler.action(cmd, handler);

            return method_to_handle_command;
        }
    }
}