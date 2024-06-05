using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services
{
    public class AccommodationPropositionService : IAccommodationPropositionService
    {
        private readonly IStatisticService _accommodationStatisticService;
        private readonly IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IReservationService _accommodationReservationService;


        public AccommodationPropositionService()
        {
            _accommodationStatisticService = Injector.Injector.CreateInstance<IStatisticService>();
            _reservationService = Injector.Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.Injector.CreateInstance<IAccommodationService>();
            _accommodationReservationService = Injector.Injector.CreateInstance<IReservationService>();
        }


        public int AccommodationWithMostReservations(int userId)
        {
            var accommodationWithMaxReservations = _accommodationService.GetAccommodationsByOwnerId(userId)
           .Select(accommodation => new
           {
               AccommodationId = accommodation.Id,
               ReservationCount = _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodation.Id).Count()
           })
           .OrderBy(accommodation => accommodation.ReservationCount)
           .LastOrDefault();

            if (accommodationWithMaxReservations == null)
            {
                // Handle the case where there are no accommodations
                return -1;
            }

            return accommodationWithMaxReservations.AccommodationId;
        }

        public int AccommodationWithLeastReservations(int userId)
        {

           var accommodationWithMinReservations = _accommodationService.GetAccommodationsByOwnerId(userId)
            .Select(accommodation => new
            {
                AccommodationId = accommodation.Id,
                ReservationCount = _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodation.Id).Count()
            })
            .OrderBy(accommodation => accommodation.ReservationCount)
            .FirstOrDefault();

            if (accommodationWithMinReservations == null)
            {
                // Handle the case where there are no accommodations
                return -1;
            }

            return accommodationWithMinReservations.AccommodationId;

        }

        public int HotAccommodationStatistics(int userId)
        {
            var userAccommodations = _accommodationService.GetAccommodationsByOwnerId(userId);

            var hottestAccommodation = userAccommodations
                .Select(accommodation => new
                {
                    AccommodationId = accommodation.Id,
                    MaxDaysOccupied = _accommodationStatisticService.YearStatisticsForAccommodation(accommodation.Id)
                        .Where(stat => stat.Year == DateTime.Now.Year)
                        .Max(stat => (int?)stat.DaysOccupied) ?? 0
                })
                .OrderByDescending(accommodation => accommodation.MaxDaysOccupied)
                .FirstOrDefault();

            return hottestAccommodation?.AccommodationId ?? -1;

        }


        public int ColdAccommodationStatistics(int userId)
        {
            var userAccommodations = _accommodationService.GetAccommodationsByOwnerId(userId);

            var coldestAccommodation = userAccommodations
                .Select(accommodation => new
                {
                    AccommodationId = accommodation.Id,
                    MaxDaysOccupied = _accommodationStatisticService.YearStatisticsForAccommodation(accommodation.Id)
                        .Where(stat => stat.Year == DateTime.Now.Year)
                        .Min(stat => (int?)stat.DaysOccupied) ?? 0
                })
                .OrderByDescending(accommodation => accommodation.MaxDaysOccupied)
                .FirstOrDefault();

            return coldestAccommodation?.AccommodationId ?? -1;

        }

    }
}
