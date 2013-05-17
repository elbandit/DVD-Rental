using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agatha.DVDRental.Catalogue.Catalogue;
using Agatha.DVDRental.Fulfillment.ApplicationService.BusinessUseCases;
using Agatha.DVDRental.Infrastructure;

namespace Agatha.DVDRental.Fulfillment.ApplicationService.Handlers
{
    public class AddFilmToCatalogueHandler : IBusinessUseCaseHandler<AddFilmToCatalogue>
    {
        private IFilmRepository _filmRepository;

        public AddFilmToCatalogueHandler(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public void action(AddFilmToCatalogue businessUseCase)
        {
            var film = new Film(businessUseCase.ReleaseDate, businessUseCase.Title);

            _filmRepository.Add(film);
        }
    }
}
