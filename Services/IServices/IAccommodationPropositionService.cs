using BookingApp.Dto;
using System.Collections.Generic;

namespace BookingApp.Services.IServices
{
    public interface IAccommodationPropositionService
    {

        public int AccommodationWithMostReservations(int userId);

        public int AccommodationWithLeastReservations(int userId);

        public int HotAccommodationStatistics(int userId);


        public int ColdAccommodationStatistics(int userId);


    }
}
