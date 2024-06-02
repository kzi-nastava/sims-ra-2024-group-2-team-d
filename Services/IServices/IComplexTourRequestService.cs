using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace BookingApp.Services.IServices
{
    public interface IComplexTourRequestService
    {

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest);

        public List<ComplexTourRequest> GetAll();

        public List<ComplexTourRequest> GetByUserId(int userId);


        public List<ComplexTourRequest> GetAllForGuide(User user);

        public ComplexTourRequest FindComplexTourRequestByTourRequestId(int id, ObservableCollection<ComplexTourRequest> complexTourRequests);
        public void ChangeStatusOfComplexTourRequest();

        public List<DateTime> FindUnavaliableDates(ComplexTourRequest complexTourRequest);
    }
}
