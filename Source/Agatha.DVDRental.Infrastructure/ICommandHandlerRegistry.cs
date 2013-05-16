using System;

namespace Agatha.DVDRental.Infrastructure
{
    public interface ICommandHandlerRegistry
    {
        Action<TCommand> find_handler_for<TCommand>(TCommand command) where TCommand : IBusinessUseCase;
    }
}