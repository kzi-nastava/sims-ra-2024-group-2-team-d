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

        public ITourRequestRepository TourRequestRepository { get; set; }
        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository, ITourRequestRepository tourRequestRepository) {
            ComplexTourRequestReposotry = complexTourRequestRepository;
            TourRequestRepository = tourRequestRepository;
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return ComplexTourRequestReposotry.Save(complexTourRequest);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return ComplexTourRequestReposotry.GetAll();
        }

        public List<ComplexTourRequest> GetByUserId(int userId)
        {
            List<ComplexTourRequest> complexTourRequests = ComplexTourRequestReposotry.GetByUserId(userId);
            foreach(var complexTourRequest in complexTourRequests)
            {
                complexTourRequest.TourRequests = TourRequestRepository.GetByIds(complexTourRequest.TourRequestIds);
            }
            return complexTourRequests;
        }

        public List<ComplexTourRequest> GetAllForGuide(User user)
        {
            return GetAll().Where(c => c.TourRequests.All(t => t.GuideId != user.Id)).ToList();
        }
        
        public ComplexTourRequest FindComplexTourRequestByTourRequestId(int id, ObservableCollection<ComplexTourRequest> complexTourRequests)
        {
            return complexTourRequests.FirstOrDefault(c => c.TourRequests.Any(t => t.Id == id));
        }

        public void ChangeStatusOfComplexTourRequest()
        {
            List<ComplexTourRequest> complexTourRequests = ComplexTourRequestReposotry.GetAll();
            foreach (var complexTourRequest in complexTourRequests)
            {
                complexTourRequest.TourRequests = TourRequestRepository.GetByIds(complexTourRequest.TourRequestIds);
                ChangeStatusToAccepted(complexTourRequest);
                InvalidateOutDatedRequests(complexTourRequest);
            }

        }

        private void InvalidateOutDatedRequests(ComplexTourRequest complexTourRequest)
        {
            List<TourRequest> tourRequests = TourRequestRepository.GetAllOutdatedPartsOfComplexTourRequest(complexTourRequest.TourRequestIds);
            if (tourRequests.Any())
            {
                foreach(var tourRequest in tourRequests)
                {
                    tourRequest.CurrentStatus = Status.Invalid;
                    TourRequestRepository.Update(tourRequest);
                }
                complexTourRequest.CurrentStatus = Status.Invalid;
                ComplexTourRequestReposotry.Update(complexTourRequest);
            }
        }

        private void ChangeStatusToAccepted(ComplexTourRequest complexTourRequest)
        {
            if (TourRequestRepository.IsEveryPartOfComplexTourAccepted(complexTourRequest.TourRequestIds))
            {
                complexTourRequest.CurrentStatus = Status.Accepted;
                ComplexTourRequestReposotry.Update(complexTourRequest);
            }
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
