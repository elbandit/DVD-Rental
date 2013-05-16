namespace Agatha.DVDRental.Infrastructure
{
    public interface IBusinessUseCaseHandler<T> where T : IBusinessUseCase
    {
        void action(T businessUseCase);
    }
}