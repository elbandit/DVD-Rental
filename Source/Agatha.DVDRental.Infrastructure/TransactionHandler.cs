using System;
using Raven.Client;

namespace Agatha.DVDRental.Infrastructure
{
    public class TransactionHandler
    {
        private readonly IDocumentSession _unitOfWork;

        public TransactionHandler(IDocumentSession unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void action<T>(T command, IBusinessUseCaseHandler<T> businessUseCaseHandler) where T : IBusinessUseCase
        {           
            using (_unitOfWork)
            {
                businessUseCaseHandler.action(command);

                _unitOfWork.SaveChanges();           
                _unitOfWork.Dispose();
            }            
        }
    }
}