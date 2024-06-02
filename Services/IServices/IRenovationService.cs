using BookingApp.Domain.Model;
using System.Collections.Generic;
using System;

namespace BookingApp.Services.IServices
{
    public interface IRenovationService
    {
         DateTime GetCheckInDate(int userId, int reservationId);
        DateTime GetCheckOutDate(int userId, int reservationId);

         Renovation Save(Renovation Renovation);

    
    }
}
