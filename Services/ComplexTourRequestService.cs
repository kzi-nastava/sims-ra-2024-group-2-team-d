using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ComplexTourRequestService
    {
        public IComplexTourRequestRepository ComplexTourRequestReposotry {  get; set; }
        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository) {
            ComplexTourRequestReposotry = complexTourRequestRepository;
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return ComplexTourRequestReposotry.Save(complexTourRequest);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return ComplexTourRequestReposotry.GetAll();
        }

        public List<ComplexTourRequest> GetAllForGuide(User user)
        {
            return GetAll().Where(c => c.TourRequests.All(t => t.GuideId != user.Id)).ToList();
        }
        
        public ComplexTourRequest FindComplexTourRequestByTourRequestId(int id, ObservableCollection<ComplexTourRequest> complexTourRequests)
        {
            return complexTourRequests.FirstOrDefault(c => c.TourRequests.Any(t => t.Id == id));
        }

        public List<DateTime> FindUnavaliableDates(ComplexTourRequest complexTourRequest)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach(var tourRequest in complexTourRequest.TourRequests)
            {
                if(tourRequest.CurrentStatus == Status.Accepted)
                {
                    dates.Add(tourRequest.ChosenDateTime);
                }
            }
            return dates;
        }

    }
}
