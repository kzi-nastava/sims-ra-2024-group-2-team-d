using BookingApp.Domain.Model;
using BookingApp.Services.IServices;
using System.Collections.Generic;
using System;
using BookingApp.Dto;
using System.Linq;

namespace BookingApp.Services
{
    public class StatisticService : IStatisticService
    {
         
        private readonly IReservationService _reservationService;
        private readonly INotificationService _notificationService;
        private readonly IRenovationRecommendationService _renovationRecommendationService;
        private readonly IChangeReservationRequestService _changeReservationRequestService;

        public StatisticService()
        {
            _reservationService = new ReservationService();
            _notificationService = new NotificationService();
            _renovationRecommendationService = Injector.Injector.CreateInstance<IRenovationRecommendationService>();
            _changeReservationRequestService = Injector.Injector.CreateInstance<IChangeReservationRequestService>();
        }

        public List<Reservation> ReservationsByAccommodationIdAndYear(int accommodationId, int year)
        {
            List<Reservation> reservationsById = ReservationsByAccommodationID(accommodationId);
            List<Reservation> finalReservationList = new List<Reservation>();

            foreach (Reservation reservation in reservationsById)
            {
                if (reservation.ReservationDateRange.StartDate.Year == year || reservation.ReservationDateRange.EndDate.Year == year)
                {
                    finalReservationList.Add(reservation);
                }
            }

            return finalReservationList;
        }

        public List<Reservation> ReservationsByAccommodationID(int accommodationID)
        {
            List<int> ReservationIds = _reservationService.GetReservationsIdsByAccommodationId(accommodationID);
            List<Reservation> Reservations = new List<Reservation>();

            foreach (int reservationId in ReservationIds)
            {
                Reservation reservation = _reservationService.GetById(reservationId);
                Reservations.Add(reservation);
            }

            return Reservations;
        }


        public List<YearStatisticDto> YearStatisticsForAccommodation(int accommodationID)
        {
            List<YearStatisticDto> statisticsByYear = new List<YearStatisticDto>();

            List<Reservation> reservations = new List<Reservation>();
            reservations = ReservationsByAccommodationID(accommodationID);

            foreach (Reservation reservation in reservations)
            {
                YearStatisticDto newStatistics = Years(reservation.ReservationDateRange.StartDate.Year, statisticsByYear);
                newStatistics.NumberOfReservations += 1;

                if (_notificationService.IsReservationCanceled(reservation.Id))
                {
                    newStatistics.NumberOfCanceledReservations += 1;
                }
            }
            YearlyRenovationRecommendationNumber(statisticsByYear, accommodationID);
            YearlyChangeReservationRequestNumber(statisticsByYear, accommodationID);
            YearDaysOccupied(statisticsByYear, reservations);

            return statisticsByYear;
        }

        public YearStatisticDto Years(int year, List<YearStatisticDto> statisticsByYearDTOs)
        {
            foreach (YearStatisticDto sby in statisticsByYearDTOs)
            {
                if (sby.Year == year)
                {
                    return sby;
                }
            }

            YearStatisticDto stats = new YearStatisticDto();
            stats.Year = year;
            statisticsByYearDTOs.Add(stats);
            return stats;
        }

        public void YearlyRenovationRecommendationNumber(List<YearStatisticDto> statisticsByYearDTOs, int accommodationId)
        {
            List<RenovationRecommendation> renvationRecommendations = _renovationRecommendationService.GetAllRecommendationsByAccommodationId(accommodationId);

            foreach (RenovationRecommendation rr in renvationRecommendations)
            {
                YearStatisticDto statistic = Years(rr.RecommendationDate.Year, statisticsByYearDTOs);
                statistic.ReccommendationForRenovations += 1;
            }
        }

        public void YearlyChangeReservationRequestNumber(List<YearStatisticDto> statisticsByYearDTOs, int accommodationId)
        {
            List<ChangeReservationRequest> changeReservationRequests = _changeReservationRequestService.GetRequestsByAccommodationId(accommodationId);
            foreach (ChangeReservationRequest crr in changeReservationRequests)
            {
                YearStatisticDto statistic = Years(crr.NewStartDate.Year, statisticsByYearDTOs);
                if (crr.RequestStatus.ToString() == "Approved")
                {
                    statistic.NumberOfRescheduledReservations += 1;
                }
            }
        }

        public void YearDaysOccupied(List<YearStatisticDto> statistics, List<Reservation> reservations)
        {

            foreach (Reservation reservation in reservations)
            {
                YearStatisticDto statisticsByYearDTO = Years(reservation.ReservationDateRange.StartDate.Year, statistics);

                int daysOccupied = 0;
                int daysOccupied1 = 0;
                if (reservation.Status == ReservationStatus.Finished)
                {
                    if (reservation.ReservationDateRange.EndDate.Year == reservation.ReservationDateRange.StartDate.Year)
                    {
                        TimeSpan duration = reservation.ReservationDateRange.EndDate.Subtract(reservation.ReservationDateRange.StartDate);
                        daysOccupied = duration.Days;
                        statisticsByYearDTO.DaysOccupied += daysOccupied;
                    }
                    else
                    {
                        int year = statisticsByYearDTO.Year;
                        DateTime lastDayOfYear = new DateTime(year, 12, 31);
                        TimeSpan duration = lastDayOfYear.Subtract(reservation.ReservationDateRange.StartDate);
                        daysOccupied = duration.Days;
                        statisticsByYearDTO.DaysOccupied += daysOccupied;

                        YearStatisticDto statisticsByYearDTONextYear = Years(year + 1, statistics);
                        int year1 = statisticsByYearDTONextYear.Year;
                        DateTime firstDayOfYear = new DateTime(year1, 1, 1);
                        TimeSpan duration1 = reservation.ReservationDateRange.EndDate.Subtract(firstDayOfYear);
                        daysOccupied1 = duration1.Days;
                        statisticsByYearDTONextYear.DaysOccupied += daysOccupied1;
                    }
                }
            }
        }

        public YearStatisticDto MostOccupiedYear(List<YearStatisticDto> statistics)
        {
            YearStatisticDto statisticWithHighestOccupation = null;
            int maxRemainingDays = int.MaxValue;

            foreach (YearStatisticDto statistic in statistics)
            {
                int days = 0;
                days = 365 - statistic.DaysOccupied;
                if (days > maxRemainingDays)
                {
                    maxRemainingDays = days;
                    statisticWithHighestOccupation = statistic;
                }
            }

            return statisticWithHighestOccupation;
        }

        public List<MonthStatisticDto> MonthStatisticsForYear(int accommodationId, int year)
        {
            List<MonthStatisticDto> monthlyStatistics = new List<MonthStatisticDto>();
            List<Reservation> reservations = new List<Reservation>();

            reservations = ReservationsByAccommodationIdAndYear(accommodationId, year);

            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationDateRange.StartDate.Year == year)
                {
                    MonthStatisticDto newStatistic = Month(monthlyStatistics, reservation.ReservationDateRange.StartDate.Month, year);
                    newStatistic.NumberOfReservations += 1;

                    if (_notificationService.IsReservationCanceled(reservation.Id))
                    {
                        newStatistic.NumberOfCanceledReservations += 1;
                    }

                }
            }

            MonthlyRenovationRecommendationNumber(monthlyStatistics, accommodationId, year);
            MonthlyChangeReservationRequestNumber(monthlyStatistics, accommodationId, year);
            MonthDaysOccupied(monthlyStatistics, reservations, year);


            var filteredStatistics =  FilterListByYear(monthlyStatistics, year);
            var fullStatistics = AddMissingMonthsStatistics(filteredStatistics, accommodationId,year);

            return fullStatistics;

        }

        public List<MonthStatisticDto> FilterListByYear(List<MonthStatisticDto> statisticsByMonthDTOs, int year)
        {
            List<MonthStatisticDto> statistics = new List<MonthStatisticDto>();
            foreach (MonthStatisticDto statistic in statisticsByMonthDTOs)
            {
                if (statistic.Year == year)
                {
                    statistics.Add(statistic);
                }
            }

            return statistics;
        }

        public List<MonthStatisticDto> AddMissingMonthsStatistics(List<MonthStatisticDto> statisticsByMonthDTOs, int accommodationId, int year)
        {
            for (int i = 1; i<=12; i++)
            {
                if (!statisticsByMonthDTOs.Any(s =>s.Month == i))
                {
                    statisticsByMonthDTOs.Add(new MonthStatisticDto(accommodationId, year,i,0,0,0,0));
                }
            }

            return statisticsByMonthDTOs.OrderBy(s=>s.Month).ToList();
        }

        public MonthStatisticDto Month(List<MonthStatisticDto> statisticsByMonth, int month, int year)
        {
            foreach (MonthStatisticDto sbm in statisticsByMonth)
            {
                if (sbm.Month == month && sbm.Year == year)
                {
                    return sbm;
                }
            }

            MonthStatisticDto statistic = new MonthStatisticDto();
            statistic.Month = month;
            statistic.Year = year;
            statisticsByMonth.Add(statistic);
            return statistic;
        }


        public void MonthlyRenovationRecommendationNumber(List<MonthStatisticDto> statisticsByMonthDTOs, int accommodationId, int year)
        {
            List<RenovationRecommendation> renvationRecommendations = _renovationRecommendationService.GetAllRecommendationsByAccommodationId(accommodationId);

            foreach (RenovationRecommendation rr in renvationRecommendations)
            {
                if (rr.RecommendationDate.Year == year)
                {
                    MonthStatisticDto statistic = Month(statisticsByMonthDTOs, rr.RecommendationDate.Month, year);
                    statistic.ReccommendationForRenovations += 1;
                }
            }
        }


        public void MonthlyChangeReservationRequestNumber(List<MonthStatisticDto> statisticsByMonthDTOs, int accommodationId, int year)
        {
            List<ChangeReservationRequest> changeReservationRequests = _changeReservationRequestService.GetRequestsByAccommodationId(accommodationId);
            foreach (ChangeReservationRequest crr in changeReservationRequests)
            {
                if (crr.NewStartDate.Year == year)
                {
                    MonthStatisticDto statistic = Month(statisticsByMonthDTOs, crr.NewStartDate.Month, year);
                    if (crr.RequestStatus.ToString() == "Approved")
                    {
                        statistic.NumberOfRescheduledReservations += 1;
                    }
                }
            }
        }

        public MonthStatisticDto MostOccupiedMonth(List<MonthStatisticDto> statistics)
        {
            MonthStatisticDto statisticWithHighestOccupation = null;
            int maxRemainingDays = int.MaxValue;

            foreach (MonthStatisticDto statistic in statistics)
            {
                int days = 0;
                days = 365 - statistic.DaysOccupied;
                if (days > maxRemainingDays)
                {
                    maxRemainingDays = days;
                    statisticWithHighestOccupation = statistic;
                }
            }

            return statisticWithHighestOccupation;
        }

        public void MonthDaysOccupied(List<MonthStatisticDto> statistics, List<Reservation> reservations, int year)
        {
            foreach (Reservation reservation in reservations)
            {
                MonthStatisticDto statisticsByMonthDTO = Month(statistics, reservation.ReservationDateRange.StartDate.Month, year);

                int daysOccupied = 0;
                int daysOccupied1 = 0;
                if (reservation.Status == ReservationStatus.Finished)
                {
                    if (reservation.ReservationDateRange.EndDate.Year == reservation.ReservationDateRange.StartDate.Year && reservation.ReservationDateRange.EndDate.Month == reservation.ReservationDateRange.StartDate.Month)
                    {
                        TimeSpan duration = reservation.ReservationDateRange.EndDate.Subtract(reservation.ReservationDateRange.StartDate);
                        daysOccupied = duration.Days;
                        statisticsByMonthDTO.DaysOccupied += daysOccupied;
                    }

                    else if (reservation.ReservationDateRange.EndDate.Year != reservation.ReservationDateRange.StartDate.Year || reservation.ReservationDateRange.EndDate.Month != reservation.ReservationDateRange.StartDate.Month)
                    {
                        if (reservation.ReservationDateRange.StartDate.Month == 12 && reservation.ReservationDateRange.EndDate.Month != 12)
                        {
                            if (reservation.ReservationDateRange.StartDate.Year == year)
                            {
                                DateTime endDate = new DateTime(statisticsByMonthDTO.Year + 1, 1, 1);
                                TimeSpan time = endDate.Subtract(reservation.ReservationDateRange.StartDate);
                                daysOccupied = time.Days;
                                statisticsByMonthDTO.DaysOccupied += daysOccupied;
                            }
                            else
                            {
                                MonthStatisticDto statisticsByMonthDTONextYear = Month(statistics, 1, year);
                                DateTime firstDayOfJanuary = new DateTime(statisticsByMonthDTONextYear.Year, 1, 1);
                                TimeSpan time1 = reservation.ReservationDateRange.EndDate.Subtract(firstDayOfJanuary);
                                daysOccupied1 = time1.Days;
                                statisticsByMonthDTONextYear.DaysOccupied += daysOccupied1;

                            }

                        }
                        else
                        {
                            int month = statisticsByMonthDTO.Month;
                            DateTime endDateMonth = new DateTime(statisticsByMonthDTO.Year, month + 1, 1);
                            TimeSpan duration = endDateMonth.Subtract(reservation.ReservationDateRange.StartDate);
                            daysOccupied = duration.Days;
                            statisticsByMonthDTO.DaysOccupied += daysOccupied;

                            MonthStatisticDto statisticsByMonthDTONextMonth = Month(statistics, month + 1, year);
                            int month1 = statisticsByMonthDTONextMonth.Month;
                            DateTime firstDayOfMonth = new DateTime(statisticsByMonthDTONextMonth.Year, month1, 1);
                            TimeSpan duration1 = reservation.ReservationDateRange.EndDate.Subtract(firstDayOfMonth);
                            daysOccupied1 = duration1.Days;
                            statisticsByMonthDTONextMonth.DaysOccupied += daysOccupied1;
                        }

                    }

                }
            }
        }
    }
}
