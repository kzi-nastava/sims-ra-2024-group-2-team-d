using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.IServices
{
    public interface IStatisticService
    {
        List<YearStatisticDto> YearStatisticsForAccommodation(int accommodationID);
        List<MonthStatisticDto> MonthStatisticsForYear(int accommodationId, int year);
        YearStatisticDto MostOccupiedYear(List<YearStatisticDto> statistics);
        MonthStatisticDto MostOccupiedMonth(List<MonthStatisticDto> statistics);
    }
}
